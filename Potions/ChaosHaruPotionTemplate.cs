using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Potions;

public abstract class ChaosHaruPotionTemplate : ModPotionTemplate
{
    public override PotionAssetProfile AssetProfile { get; } = PotionAssetProfile.Empty;
}
