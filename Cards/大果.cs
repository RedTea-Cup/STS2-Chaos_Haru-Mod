using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;

// 大果
// 费用: 1
// 效果：减少5层压力。消耗。
// 升级：获得保留。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class DaguoCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust,
    ];

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<YaliPower>(5m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<YaliPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var yali = Owner.Creature.GetPower<YaliPower>();
        if (yali != null)
            await PowerCmd.ModifyAmount(yali, -DynamicVars["YaliPower"].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}