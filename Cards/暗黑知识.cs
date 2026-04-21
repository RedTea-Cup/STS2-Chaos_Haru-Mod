using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 暗黑知识
// 费用: 0
// 效果：抽1张牌并将其打出。结束你的回合。
// 升级：不再消耗。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class AnheizhishiCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var drawnCard = (await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false)).FirstOrDefault();
        if (drawnCard != null)
            await CardCmd.AutoPlay(choiceContext, drawnCard, null, AutoPlayType.Default, false, false);

        PlayerCmd.EndTurn(Owner, false, null);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}
