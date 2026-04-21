using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class ZifadianshiyanPower : ModPowerTemplate
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("ChargeStep", 1m),
    ];
    
    public override PowerType Type => PowerType.Buff;
    
    public override PowerStackType StackType => PowerStackType.Single;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/ZifadianshiyanDa.png",
            Const.Paths.Root + "/images/powers/ZifadianshiyanXiao.png");

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player || Amount <= 0)
            return;

        Flash();
        var chongdianToGain = Amount * DynamicVars["ChargeStep"].IntValue;
        await PowerCmd.Apply<ChongdianPower>(Owner, chongdianToGain, Owner, null, false);
        DynamicVars["ChargeStep"].BaseValue += 1m;
    }
}
