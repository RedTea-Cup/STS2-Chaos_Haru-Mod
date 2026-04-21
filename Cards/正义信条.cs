using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 正义信条
// 费用: 2
// 效果：虚无。获得1层正义。
// 升级：不再虚无。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ZhengyixintiaoCard()
    : ChaosHaruCardTemplate(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ZhengyiPower>(1m)];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<ZhengyiPower>(),
        HoverTipFactory.FromPower<StrengthPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ZhengyiPower>(Owner.Creature, 1m, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Ethereal);
}
