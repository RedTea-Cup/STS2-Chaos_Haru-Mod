using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Chaos_Haru.Keywords;

namespace Chaos_Haru.Cards;
// 原子瓦解
// 费用: 2
// 效果：粉碎。对所有敌人造成18点伤害。
// 升级：伤害增加6点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class YuanziwajieCard()
    : ChaosHaruCardTemplate(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies, true)
{
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(18m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(CombatState);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(CombatState)
            .Execute(choiceContext);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!ReferenceEquals(cardSource, this))
            return 1m;

        return FensuiRules.GetDamageMultiplier(target, props, cardSource);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(6m);
}
