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
// 锚炮
// 费用: 2
// 效果：造成12点伤害。每使用一张锚炮牌，增加6点伤害。 
// 升级：造成18点伤害。每使用一张锚炮牌，增加9点伤害。
[RegisterCard(typeof(ChaosHaruCardPool))]
[RegisterCharacterStarterCard(typeof(ChaosHaruCharacter), 1)]
[RegisterArchaicToothTranscendence(typeof(Maopao2Card))]
public sealed class MaopaoCard()
    : ChaosHaruCardTemplate(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];

    private decimal _extraDamageFromMaopaoPlays;

    private decimal ExtraDamageFromMaopaoPlays
    {
        get => _extraDamageFromMaopaoPlays;
        set
        {
            AssertMutable();
            _extraDamageFromMaopaoPlays = value;
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(12m, ValueProp.Move),
        new DynamicVar("Increase", 6m),
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
        foreach (var card in owner.PlayerCombatState.AllCards.OfType<MaopaoCard>())
            card.BuffFromMaopaoPlay(increase);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(6m);
        DynamicVars["Increase"].UpgradeValueBy(3m);
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
        DynamicVars.Damage.BaseValue += ExtraDamageFromMaopaoPlays;
    }

    private void BuffFromMaopaoPlay(decimal extraDamage)
    {
        DynamicVars.Damage.BaseValue += extraDamage;
        ExtraDamageFromMaopaoPlays += extraDamage;
    }
}
