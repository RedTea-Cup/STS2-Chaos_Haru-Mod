using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 好战
// 费用: 0
// 效果：如果手牌中没有攻击牌，抽2张牌。
// 升级：抽牌增加1张。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class HaozhanCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!PileTypeExtensions.GetPile(PileType.Hand, Owner).Cards.Any(static c => c.Type == CardType.Attack))
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false);
    }

    protected override void OnUpgrade() => DynamicVars.Cards.UpgradeValueBy(1m);
}
