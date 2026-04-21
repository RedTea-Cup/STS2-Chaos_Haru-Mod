using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class ZhanlvechonggouPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/ZhanlvechonggouDa.png",
            Const.Paths.Root + "/images/powers/ZhanlvechonggouXiao.png");

    // 每当打破敌人格挡时，获得3点充电
    public override async Task AfterBlockBroken(Creature creature)
    {
        if (!creature.IsEnemy)
            return;

        Flash();
        await PowerCmd.Apply<ChongdianPower>(Owner, Amount * 3, Owner, null, false);
    }
}
