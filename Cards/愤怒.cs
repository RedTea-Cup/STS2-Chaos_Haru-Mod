using Chaos_Haru.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;

// 愤怒
// 费用: 2
// 效果：粉碎。造成15点伤害。若手牌中只有攻击牌，则耗能-2。
// 升级：造成20点伤害。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class FennuCard()
    : ChaosHaruCardTemplate(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(15m, ValueProp.Move)];

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

    // 若手牌中只有攻击牌，耗能 -2（动态实时计算，无需状态管理）
    public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (!ReferenceEquals(card, this) || Owner == null)
            return false;

        var hand = PileTypeExtensions.GetPile(PileType.Hand, Owner);
        if (hand == null || hand.Cards.Count == 0)
            return false;

        if (hand.Cards.All(static c => c.Type == CardType.Attack))
        {
            modifiedCost = Math.Max(0m, originalCost - 2m);
            return true;
        }
        return false;
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(5m);
}