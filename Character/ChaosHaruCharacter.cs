using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Chaos_Haru.Content.Descriptors;
using STS2RitsuLib.Scaffolding.Characters;
using STS2RitsuLib.Scaffolding.Godot;

namespace Chaos_Haru.Character;

[RegisterCharacter]
public sealed class ChaosHaruCharacter : ModCharacterTemplate<ChaosHaruCardPool, ChaosHaruRelicPool, ChaosHaruPotionPool>
{
    // 角色是否需要时代和时间线系统
    public override bool RequiresEpochAndTimeline => false;

    public static readonly Color DefaultColor = new("7A7AFF");

    public override Color NameColor => new(0.5f, 0.5f, 1f, 1f);

    public override Color EnergyLabelOutlineColor => new(0.1f, 0.1f, 1f, 1f);

    public override CharacterGender Gender => CharacterGender.Feminine;

    public override int StartingHp => 80;

    public override int StartingGold => 99;

    public override CharacterAssetProfile AssetProfile => HaruCharacterAssets.Profile;

    public override Color MapDrawingColor => DefaultColor;

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    protected override Type? UnlocksAfterRunAsType => typeof(Ironclad);

    protected override NCreatureVisuals? TryCreateCreatureVisuals()
    {
        return RitsuGodotNodeFactories.CreateFromScenePath<NCreatureVisuals>(Const.Paths.CharacterVisualsScene);
    }

    public override List<string> GetArchitectAttackVfx()
    {
        return
        [
            "vfx/vfx_attack_blunt",
            "vfx/vfx_heavy_blunt",
            "vfx/vfx_attack_slash",
            "vfx/vfx_bloody_impact",
            "vfx/vfx_rock_shatter",
        ];
    }
}
