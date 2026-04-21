using STS2RitsuLib.Scaffolding.Characters;

namespace Chaos_Haru.Content.Descriptors;

internal static class HaruCharacterAssets
{
    private static readonly CharacterAssetProfile Base = CharacterAssetProfiles.Ironclad();
    private static readonly CharacterVfxAssetSet DefectTrail = CharacterAssetProfiles.Defect().Vfx!;

    internal static CharacterAssetProfile Profile { get; } = Base
        .WithScenes(Base.Scenes! with
        {
            VisualsPath = Const.Paths.CharacterVisualsScene,
        })
        .WithUi(Base.Ui! with
        {
            IconTexturePath = Const.Paths.CharacterIconTexture,
            IconPath = Const.Paths.CharacterIconScene,
            CharacterSelectBgPath = Const.Paths.CharacterSelectBg,
            CharacterSelectIconPath = Const.Paths.CharacterSelectIcon,
            CharacterSelectLockedIconPath = Const.Paths.CharacterSelectLockedIcon,
            MapMarkerPath = Const.Paths.MapMarker,
        })
        .WithVfx(DefectTrail)
        .WithAudio(Base.Audio! with
        {
            CharacterTransitionSfx = Const.Paths.CharacterTransitionSfx,
        });
}
