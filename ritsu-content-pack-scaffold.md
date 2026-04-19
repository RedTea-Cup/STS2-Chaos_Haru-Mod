# Ritsu content-pack scaffold

- Generated from source patterns after removing the BaseLib project dependency.
- This scaffold does not recreate BaseLib APIs; it only proposes RitsuLib registration calls.
- You still need to rewrite BaseLib-based model classes to native game or Ritsu-compatible types before this code can compile.

## Suggested registration skeleton

```csharp
RitsuLibFramework.CreateContentPack("Chaos_Haru")
    .Character<Chaos_Haru.Scripts.Characters.Chaos_HaruCharacter>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.DefendCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.FengliyijiCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.FensuidajiCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.MaodianCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.MaopaoCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.StrikeCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.XuanfengmaoCard>()
    .Card<Chaos_Haru.Scripts.CardPools.Chaos_HaruCardPool, Chaos_Haru.Scripts.Cards.ZhengyixintiaoCard>()
    .Relic<Chaos_Haru.Scripts.RelicPools.Chaos_HaruRelicPool, Chaos_Haru.Scripts.Relics.Haru2Relic>()
    .Relic<Chaos_Haru.Scripts.RelicPools.Chaos_HaruRelicPool, Chaos_Haru.Scripts.Relics.HaruRelic>()
    .Potion<Chaos_Haru.Scripts.PotionPools.Chaos_HaruPotionPool, Chaos_Haru.Scripts.SoulPotion>()
    .Power<Chaos_Haru.Scripts.Powers.ZhengyiPower>()
    .Apply();

var content = RitsuLibFramework.GetContentRegistry("Chaos_Haru");
// No custom monster classes were detected.
```

## Detected types

- Characters: 1
- Pool-bound cards: 8
- Pool-bound relics: 2
- Pool-bound potions: 1
- Powers: 1
- Monsters: 0
- Encounters: 0
- Ancients: 0

## Content model inventory

- These classes were detected as still inheriting BaseLib content abstractions or depending on BaseLib pool markers.
- Treat this as the rewrite backlog after dependency conversion and Harmony bootstrap migration.

### Shared Card Pools

- None detected.

### Shared Relic Pools

- None detected.

### Shared Potion Pools

- None detected.

### Characters

- Chaos_Haru.Scripts.Characters.Chaos_HaruCharacter | base: PlaceholderCharacterModel | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Characters\小春.cs

### Cards

- Chaos_Haru.Scripts.Cards.DefendCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\防御.cs
- Chaos_Haru.Scripts.Cards.FengliyijiCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\奋力一击.cs
- Chaos_Haru.Scripts.Cards.FensuidajiCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\粉碎之锚.cs
- Chaos_Haru.Scripts.Cards.MaodianCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚点.cs
- Chaos_Haru.Scripts.Cards.MaopaoCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\锚炮.cs
- Chaos_Haru.Scripts.Cards.StrikeCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\打击.cs
- Chaos_Haru.Scripts.Cards.XuanfengmaoCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\旋风锚.cs
- Chaos_Haru.Scripts.Cards.ZhengyixintiaoCard | base: CustomCardModel | pool: Chaos_HaruCardPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Cards\正义信条.cs

### Relics

- Chaos_Haru.Scripts.Relics.Haru2Relic | base: CustomRelicModel | pool: Chaos_HaruRelicPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物升级.cs
- Chaos_Haru.Scripts.Relics.HaruRelic | base: CustomRelicModel | pool: Chaos_HaruRelicPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Relics\初始遗物.cs

### Potions

- Chaos_Haru.Scripts.SoulPotion | base: CustomPotionModel | pool: Chaos_HaruPotionPool | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Potions\灵魂药水.cs

### Powers

- Chaos_Haru.Scripts.Powers.ZhengyiPower | base: CustomPowerModel | file: D:\mod\ctf9\tools\STS2-Chaos_Haru-Mod-main\Scripts\Powers\正义.cs

### Monsters

- None detected.

### Encounters

- None detected.

### Ancients

- None detected.

