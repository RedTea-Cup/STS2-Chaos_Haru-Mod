using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace Chaos_Haru.Scripts.Keywords;
public class FensuiKeywords
{
    // 自定义枚举的名字。
    [CustomEnum("FENSUI")]

    // 放在原版卡牌描述的位置，这里是卡牌描述的前面
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Fensui;
}