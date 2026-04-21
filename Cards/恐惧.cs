using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace Chaos_Haru.Cards;
// 恐惧
// 费用: 0
// 类型: 诅咒
// 效果：如果这张牌在你的手牌中，你必须优先打出这张牌。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class KongjuCard()
    : ChaosHaruCardTemplate(0, CardType.Curse, CardRarity.Curse, TargetType.None, true)
{
    public override bool CanBeGeneratedByModifiers => false;

    protected override bool ShouldGlowRedInternal => true;

    public override int MaxUpgradeLevel => 0;

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Eternal,
        CardKeyword.Exhaust
    ];

    public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType)
    {
        if (card.Owner != Owner)
            return true;

        var pile = Pile;
        if (pile?.Type != PileType.Hand)
            return true;

        if (card is KongjuCard)
            return true;

        if (autoPlayType != AutoPlayType.None)
            return true;

        return false;
    }
}
