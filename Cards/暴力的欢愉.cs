using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using STS2RitsuLib.Scaffolding.Characters;

namespace Chaos_Haru.Cards;
// 暴力的欢愉
// 费用: 1
// 效果：将自身的压力全部转换为充电。
// 升级：费用减少1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class BaolidehuanyuCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<YaliPower>(),
        HoverTipFactory.FromPower<ChongdianPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var yaliAmount = Owner.Creature.GetPowerAmount<YaliPower>();
        if (yaliAmount <= 0)
            return;

        await PowerCmd.Remove<YaliPower>(Owner.Creature);
        await PowerCmd.Apply<ChongdianPower>(Owner.Creature, yaliAmount, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}