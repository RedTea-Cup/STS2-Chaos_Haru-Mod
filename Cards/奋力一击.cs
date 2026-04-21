using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 奋力一击
// 费用: 6
// 效果：造成10点伤害。\n自身每层正面增益使伤害增加10点。
// 升级：费用减少1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class FengliyijiCard()
    : ChaosHaruCardTemplate(6, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CalculationBaseVar(10m),
        new ExtraDamageVar(10m),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) =>
            card.Owner.Creature.Powers.Where(static p => p.TypeForCurrentAmount == PowerType.Buff && p.Amount > 0)
                .Sum(static p => p.Amount)),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}
