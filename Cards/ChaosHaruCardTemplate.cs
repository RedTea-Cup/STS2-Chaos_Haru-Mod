using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Cards;
// 这是一个卡牌模板类，所有Chaos Haru的卡牌都应该继承自这个类。
public abstract class ChaosHaruCardTemplate(
    int baseCost,
    CardType type,
    CardRarity rarity,
    TargetType targetType,
    bool shouldShowInCardLibrary = true)
    : ModCardTemplate(baseCost, type, rarity, targetType, shouldShowInCardLibrary)
{
    public override CardAssetProfile AssetProfile =>
        new(Const.Paths.Card(GetType().Name), Const.Paths.Card(GetType().Name));
}
