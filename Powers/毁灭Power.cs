using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class HuimiePower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/HuimieDa.png",
            Const.Paths.Root + "/images/powers/HuimieXiao.png");

    // 回合开始：获得1点压力，抽1张牌
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player || Amount <= 0)
            return;

        Flash();
        await PowerCmd.Apply<YaliPower>(Owner, Amount, Owner, null, false);
        await CardPileCmd.Draw(choiceContext, Amount, player);
    }
}