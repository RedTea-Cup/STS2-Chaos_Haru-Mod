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
            RestSiteAnimPath = Const.Paths.RestSiteAnim,// 休息点动画
            MerchantAnimPath = Const.Paths.MerchantAnim,// 商人动画
        })
        .WithUi(Base.Ui! with
        {
            IconTexturePath = Const.Paths.CharacterIconTexture,// 角色图标
            IconPath = Const.Paths.CharacterIconScene,// 角色图标场景
            CharacterSelectBgPath = Const.Paths.CharacterSelectBg,// 角色选择界面背景
            CharacterSelectIconPath = Const.Paths.CharacterSelectIcon,// 角色选择界面图标
            CharacterSelectLockedIconPath = Const.Paths.CharacterSelectLockedIcon,// 角色选择界面锁定图标
            MapMarkerPath = Const.Paths.MapMarker,// 地图标记
        })
        .WithVfx(DefectTrail)
        .WithAudio(Base.Audio! with
        {
            CharacterTransitionSfx = Const.Paths.CharacterTransitionSfx, // 过渡音效
        })
        .WithMultiplayer(Base.Multiplayer! with
        {
            ArmPointingTexturePath = Const.Paths.ArmPointingTexture,
            ArmRockTexturePath = Const.Paths.ArmRockTexture,
            ArmPaperTexturePath = Const.Paths.ArmPaperTexture,
            ArmScissorsTexturePath = Const.Paths.ArmScissorsTexture,
        });
}
