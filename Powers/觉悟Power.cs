using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class JuewuPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/JuewuDa.png",
            Const.Paths.Root + "/images/powers/JuewuXiao.png");

    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power.Owner != Owner || power is not StrengthPower || amount <= 0m)
            return;

        Flash();
        await CreatureCmd.GainBlock(Owner, amount * Amount, ValueProp.Unpowered, null);
    }
}