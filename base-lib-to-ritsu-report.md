# BaseLib -> RitsuLib migration report

## Scope

- This converter removes the BaseLib project/package dependency and points the project to the STS2.RitsuLib NuGet package.
- It can generate project-local compatibility shims for commonly used BaseLib abstract/config/utils APIs.
- Migrated projects also get STS001/STS003 analyzer suppression because the stock analyzers do not understand the generated BaseLib compatibility surface.

## Dependency target

- Project root: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main
- Old library root: D:\mod\ctf9\tools\BaseLib-StS2-master
- RitsuLib package id: STS2.RitsuLib
- RitsuLib package version: 0.0.54
- RitsuLib metadata source: D:\mod\ctf9\tools\STS2-RitsuLib-main
- New manifest id: STS2-RitsuLib
- Project-local Sts2PathDiscovery.props: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Sts2PathDiscovery.props
- Safe C# rewrites enabled: True
- Legacy Harmony bootstrap requested: True
- Patch bootstrap rewrite enabled: True
- Migration support requested: True
- Migration support rewrite enabled: True
- Ritsu scaffold enabled: True
- Legacy Harmony bootstrap path: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Generated\BaseLibToRitsu\LegacyHarmonyPatchBootstrap.g.cs
- Migration support path: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Generated\BaseLibToRitsu\LegacyMigrationSupport.g.cs
- Compatibility support path: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Generated\BaseLibToRitsu\LegacyCompatibility.g.cs

## Changed files

- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Chaos_Haru.csproj: enabled deploy targets after Build when present; enabled sts2 publicizer for generated compatibility shims; ensured project-local Sts2PathDiscovery.props is imported; made hardcoded STS2 path properties overridable; removed incompatible package source exclusions for publicizer; suppressed BaseLib migration analyzer noise; updated project dependency references
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮先古.cs: rewrote BaseLib.Utils usage to generated migration support
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Entry.cs: rewrote BaseLib.Utils usage to generated migration support

## Generated scaffold

- Ritsu registration scaffold was already up to date: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\ritsu-content-pack-scaffold.md
- The scaffold now includes a per-type content model inventory for the remaining BaseLib abstract classes.

## Generated migration support

- Migration support file was already up to date: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Generated\BaseLibToRitsu\LegacyMigrationSupport.g.cs
- Rewritten C# files: 2
- Compatibility support file was already up to date: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Generated\BaseLibToRitsu\LegacyCompatibility.g.cs
- Scope: BaseLib.Utils / NodeFactory wrappers, project-local SpireField support, BaseLib.Abstracts / BaseLib.Config compatibility shims, and runtime registration / asset patches.

## Legacy Harmony bootstrap

- Detected legacy Harmony patch classes: 0
- TryPatchAll call sites found before rewrite: 0
- TryPatchAll call sites rewritten: 0
- No directly patchable Harmony classes were detected, so no bootstrap file was generated.
- No legacy Harmony patch classes were detected.

## Manual Harmony patch sites

- No direct harmony.Patch(...) sites were found.

## Remaining BaseLib code references

- No BaseLib C# usages were found.

## Migration buckets

### Config and settings

- Recommendation: Rewrite to ModConfig-STS2 or native Ritsu settings. RitsuLib does not expose BaseLib.Config as a drop-in API.
- No matches found.

### Patch bootstrap

- Recommendation: Prefer the generated legacy Harmony bootstrap for generic migration: replace TryPatchAll with CreateClassProcessor(...).Patch() calls per annotated class, then review any manual harmony.Patch(...) sites separately. Full Ritsu ModPatcher migration is still manual.
- No matches found.

### Godot node factories

- Recommendation: Prefer the generated LegacyNodeFactory wrappers for generic migration, or rewrite directly to STS2RitsuLib.Scaffolding.Godot.RitsuGodotNodeFactories.
- No matches found.

### Saved runtime fields

- Recommendation: Prefer the generated project-local SpireField/SavedSpireField helpers for generic migration, or replace them with another save strategy.
- No matches found.

### Content models and pool markers

- Recommendation: Rewrite BaseLib abstract models to vanilla or Ritsu-native model types, then register them through CreateContentPack / ModContentRegistry.
- Match count: 30
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\CardPools\角色卡池.cs:5 -> public class Chaos_HaruCardPool : CustomCardPoolModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\CardPools\角色卡池.cs:22 -> // public override Texture2D? CustomFrame(CustomCardModel card)
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\奋力一击.cs:15 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\奋力一击.cs:16 -> public class FengliyijiCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\打击.cs:12 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\打击.cs:13 -> public class StrikeCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\旋风锚.cs:16 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\旋风锚.cs:17 -> public class XuanfengmaoCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\正义信条.cs:12 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\正义信条.cs:13 -> public class ZhengyixintiaoCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\粉碎之锚.cs:15 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\粉碎之锚.cs:16 -> public class FensuidajiCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮.cs:15 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮.cs:16 -> public class MaopaoCard : CustomCardModel, ITranscendenceCard
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮先古.cs:13 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮先古.cs:15 -> public class Maopao2Card : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚点.cs:11 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚点.cs:12 -> public class MaodianCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\防御.cs:12 -> [Pool(typeof(Chaos_HaruCardPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\防御.cs:13 -> public class DefendCard : CustomCardModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Characters\小春.cs:12 -> public class Chaos_HaruCharacter : PlaceholderCharacterModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\PotionPools\角色药水池.cs:4 -> public class Chaos_HaruPotionPool : CustomPotionPoolModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Potions\灵魂药水.cs:13 -> [Pool(typeof(Chaos_HaruPotionPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Potions\灵魂药水.cs:14 -> public class SoulPotion : CustomPotionModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Powers\正义.cs:10 -> public class ZhengyiPower : CustomPowerModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\RelicPools\角色遗物池.cs:4 -> public class Chaos_HaruRelicPool : CustomRelicPoolModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物.cs:13 -> [Pool(typeof(Chaos_HaruRelicPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物.cs:14 -> public class HaruRelic : CustomRelicModel
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物升级.cs:14 -> [Pool(typeof(Chaos_HaruRelicPool))]
- D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物升级.cs:15 -> public class Haru2Relic : CustomRelicModel

## Remaining documentation and manifest mentions

- No non-code BaseLib mentions were found.

## How to run

.\tools\Convert-BaseLibToRitsuLib.ps1 -ProjectRoot "D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main" -Apply -RewriteSafeCode -RewritePatchBootstrap -GenerateMigrationSupport -RewriteMigrationSupportUsings -GenerateRitsuScaffold
