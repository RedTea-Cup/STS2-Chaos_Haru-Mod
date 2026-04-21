using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Relics;

public abstract class ChaosHaruRelicTemplate : ModRelicTemplate
{
    public override string PackedIconPath => Const.Paths.Relic(GetType().Name);

    protected override string PackedIconOutlinePath => PackedIconPath;

    protected override string BigIconPath => PackedIconPath;

    public override RelicAssetProfile AssetProfile => new(PackedIconPath, PackedIconOutlinePath, BigIconPath);
}
