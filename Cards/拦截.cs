using Chaos_Haru.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 拦截
// 费用: 3
// 效果：粉碎。造成目标格挡值的伤害。
// 升级：费用减少1点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class LanjieCard()
    : ChaosHaruCardTemplate(3, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];

    // 基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars =>[
        
    ];

    // 卡牌标签
    protected override HashSet<CardTag> CanonicalTags => [
        
    ];

    // 卡牌旁出现的提示方框
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        
    ];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

        decimal damage = cardPlay.Target.Block;

        await DamageCmd.Attack(damage)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }
    // 粉碎
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!ReferenceEquals(cardSource, this))
            return 1m;

        return FensuiRules.GetDamageMultiplier(target, props, cardSource);
    }
    
    // 升级效果
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}