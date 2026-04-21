using Chaos_Haru.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class YaliPower : ModPowerTemplate
{
    private const int Threshold = 10;
    private const int KongjuCount = 5;

    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/YaliDa.png",
            Const.Paths.Root + "/images/powers/YaliXiao.png");

    // 每次层数变化后即时检查（AfterApplied 只触发一次，此钩子新建和叠加均触发）
    public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power != this)
            return;

        if (Amount < Threshold)
            return;

        // 加入5张恐惧到抽牌堆
        await CardPileCmd.AddToCombatAndPreview<KongjuCard>(
            Owner, PileType.Draw, KongjuCount, addedByPlayer: true);

        // 施加5层崩溃
        await PowerCmd.Apply<BengkuiPower>(Owner, KongjuCount, Owner, null, false);

        // 减少10层压力
        await PowerCmd.ModifyAmount(this, -Threshold, applier, cardSource);
    }
}


