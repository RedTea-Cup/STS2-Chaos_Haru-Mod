using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using Chaos_Haru.Keywords;

namespace Chaos_Haru.Cards;
// 锚炮2
// 费用: 2
// 效果：造成24点伤害。每使用一张锚炮牌，增加12点伤害。
// 升级：造成36点伤害。每使用一张锚炮牌，增加18点伤害。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class Maopao2Card()
    : ChaosHaruCardTemplate(2, CardType.Attack, CardRarity.Ancient, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];

    private decimal _extraDamageFromMaopao2Plays;

    private decimal ExtraDamageFromMaopao2Plays
    {
        get => _extraDamageFromMaopao2Plays;
        set
        {
            AssertMutable();
            _extraDamageFromMaopao2Plays = value;
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(24m, ValueProp.Move),
        new DynamicVar("Increase", 12m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitVfxNode(t => NScratchVfx.Create(t, true))
            .Execute(choiceContext);

        var owner = Owner;
        if (owner?.PlayerCombatState == null)
            return;

        var increase = DynamicVars["Increase"].BaseValue;
        foreach (var card in owner.PlayerCombatState.AllCards.OfType<Maopao2Card>())
            card.BuffFromMaopao2Play(increase);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(12m);
        DynamicVars["Increase"].UpgradeValueBy(6m);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!ReferenceEquals(cardSource, this))
            return 1m;

        return FensuiRules.GetDamageMultiplier(target, props, cardSource);
    }

    protected override void AfterDowngraded()
    {
        base.AfterDowngraded();
        DynamicVars.Damage.BaseValue += ExtraDamageFromMaopao2Plays;
    }

    private void BuffFromMaopao2Play(decimal extraDamage)
    {
        DynamicVars.Damage.BaseValue += extraDamage;
        ExtraDamageFromMaopao2Plays += extraDamage;
    }
}
