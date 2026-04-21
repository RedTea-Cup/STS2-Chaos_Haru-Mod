using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Content;
using STS2RitsuLib.Keywords;

namespace Chaos_Haru.Keywords;

/// <summary>
///     Auto-registration skips <c>abstract</c> types; <c>static class</c> compiles to abstract sealed, so keyword
///     attributes must sit on a concrete nested type (same pattern as WineFox / Neuvillette).
/// </summary>
internal static class ChaosHaruFensuiKeywordRegistration
{
    [RegisterOwnedCardKeyword("fensui", LocKeyPrefix = "FENSUI",
        CardDescriptionPlacement = ModKeywordCardDescriptionPlacement.BeforeCardDescription)]
    private sealed class FensuiKeyword;
}

public static class FensuiRules
{
    public static string FensuiKeywordId => ModContentRegistry.GetQualifiedKeywordId(Const.ModId, "fensui");

    public static decimal GetDamageMultiplier(Creature? target, ValueProp props, CardModel? cardSource)
    {
        if (!ValuePropExtensions.IsPoweredAttack(props))
            return 1m;

        return target is { Block: > 0 } ? 2m : 1m;
    }
}
