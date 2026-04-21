using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Chaos_Haru.Keywords;

namespace Chaos_Haru.Cards;
// 粉碎之锚
// 费用: 1
// 效果：粉碎。造成10点伤害。
// 升级：伤害增加3点。保留。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class FensuizhimaoCard()
    : ChaosHaruCardTemplate(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
{
    // 注册关键词
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];
    // 基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10m, ValueProp.Move)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!ReferenceEquals(cardSource, this))
            return 1m;

        return FensuiRules.GetDamageMultiplier(target, props, cardSource);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        AddKeyword(CardKeyword.Retain);
    }
}
