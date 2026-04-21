namespace Chaos_Haru;

public static class Const
{
    public const string ModId = "Chaos_Haru";
    public const string Name = "Chaos_Haru";
    public const string Version = "1.0.0";
    public const string EnergyColorName = "chaos_haru";

    public static class Paths
    {
        public const string Root = "res://Chaos_Haru";

        public const string CharacterVisualsScene = Root + "/scenes/creature_visuals/Haru.tscn";
        public const string CharacterIconTexture = Root + "/images/ui/headIcon/haru_icon.png";
        public const string CharacterIconScene = Root + "/scenes/ui/character_icons/haru_icon.tscn";
        public const string CharacterSelectBg = Root + "/scenes/beijing/beijing.tscn";
        public const string CharacterSelectIcon = Root + "/images/charui/char_select_haru.png";
        public const string CharacterSelectLockedIcon = Root + "/images/charui/char_select_haru_locked.png";
        public const string MapMarker = Root + "/images/packed/map/icons/map_marker_haru.png";
        public const string CharacterTransitionSfx = "event:/sfx/ui/wipe_ironclad";

        public const string TextEnergyIcon = Root + "/images/mp/energy.png";
        public const string BigEnergyIcon = Root + "/images/mp/big/energy_big.png";

        public static string Card(string typeName) => $"{Root}/images/cards/{typeName}.png";

        public static string Relic(string typeName) => $"{Root}/images/relics/{typeName}.png";

        public const string SoulPotionImage = Root + "/images/potions/soul_icon.svg";
    }
}
