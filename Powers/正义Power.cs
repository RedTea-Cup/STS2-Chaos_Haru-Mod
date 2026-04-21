using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class ZhengyiPower : ModPowerTemplate
{
    private decimal _strengthGainedThisTurn;

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/ZhengyiDa.png",
            Const.Paths.Root + "/images/powers/ZhengyiXiao.png");

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature == Owner && cardPlay.Card.Type == CardType.Attack)
        {
            await PowerCmd.Apply<StrengthPower>(Owner, Amount, Owner, cardPlay.Card, false);
            _strengthGainedThisTurn += Amount;
        }
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side != Owner.Side || _strengthGainedThisTurn <= 0m)
            return;

        var strengthToLose = _strengthGainedThisTurn;
        _strengthGainedThisTurn = 0m;
        Flash();
        await PowerCmd.Apply<StrengthPower>(Owner, -strengthToLose, Owner, null, false);
    }
}
