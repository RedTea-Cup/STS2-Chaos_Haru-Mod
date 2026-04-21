using Chaos_Haru.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class KouyaPower : ModPowerTemplate
{
    // 本回合已触发的返还次数
    private int _returnsUsedThisTurn;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/KouyaDa.png",
            Const.Paths.Root + "/images/powers/KouyaXiao.png");

    // 回合开始时重置本回合触发次数
    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == Owner.Player)
            _returnsUsedThisTurn = 0;
        return Task.CompletedTask;
    }

    // 每回合前 Amount 次打出锚炮/神锚炮时，将其从弃牌堆移回手牌
    public override async Task AfterCardPlayedLate(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != Owner)
            return;

        if (cardPlay.Card is not (MaopaoCard or Maopao2Card))
            return;

        if (_returnsUsedThisTurn >= Amount)
            return;

        _returnsUsedThisTurn++;

        // AfterCardPlayedLate 时卡牌仍在 Play 堆，通过 ResultPile 确认将进入弃牌堆
        // 直接将其移动到手牌，覆盖默认的入弃牌堆行为
        if (cardPlay.ResultPile == PileType.Discard)
        {
            Flash();
            await CardPileCmd.Add(cardPlay.Card, PileType.Hand);
        }
    }
}