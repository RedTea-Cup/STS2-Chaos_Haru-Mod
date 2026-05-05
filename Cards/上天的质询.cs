using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Chaos_Haru.Cards;
// 上天的质询
// 费用: 0
// 效果：将所有牌堆中所有的打击牌和防御牌变化为巨石。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ShangtiandezhixunCard()
    : ChaosHaruCardTemplate(0, CardType.Power, CardRarity.Rare, TargetType.Self, true)
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromCard<GiantRock>(IsUpgraded),
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var cards = Owner.PlayerCombatState!.AllCards
            .Where(c => c != null && c.IsTransformable &&
                (c.Tags.Contains(CardTag.Strike) || c.Tags.Contains(CardTag.Defend)))
            .ToList();

        foreach (var item in cards)
        {
            var giantRock = CombatState!.CreateCard<GiantRock>(Owner);
            if (IsUpgraded)
                CardCmd.Upgrade(giantRock);
            await CardCmd.Transform(item, giantRock);
        }
    }
}