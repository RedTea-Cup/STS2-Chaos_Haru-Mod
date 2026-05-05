using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Chaos_Haru.Cards;

// 启示
// 费用: 1
// 效果：选择一张手牌，使其在本场战斗中耗能-1。
// 升级：费用降为0。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class QishiCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var selected = (await CardSelectCmd.FromHand(
            choiceContext,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, 1),
            c => c != this,
            this)).FirstOrDefault();

        if (selected != null)
            selected.EnergyCost.AddThisCombat(-1);
    }

    protected override void OnUpgrade() => base.EnergyCost.UpgradeBy(-1);
}