using BaseLibToRitsu.Generated;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Scripts.Keywords;
public class FensuiKeywords
{
    // 自定义枚举的名字。
    [CustomEnum("FENSUI")]
    // 放在原版卡牌描述的位置，这里是卡牌描述的前面
    //[KeywordProperties(AutoKeywordPosition.Before)]

    public static CardKeyword Fensui;

    public static decimal GetDamageMultiplier(Creature? target, ValueProp props, CardModel? cardSource)
    {
        if (cardSource == null)
        {
            return 1m;
        }

        if (!cardSource.Keywords.Contains(Fensui))
        {
            return 1m;
        }

        if (!props.IsPoweredAttack())
        {
            return 1m;
        }

        return target is { Block: > 0 } ? 2m : 1m;
    }
}