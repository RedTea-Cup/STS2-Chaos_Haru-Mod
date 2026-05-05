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
        // 角色资源 战斗中的人物
        public const string CharacterVisualsScene = Root + "/scenes/creature_visuals/Haru.tscn";
        // UI资源 头像
        public const string CharacterIconTexture = Root + "/images/ui/headIcon/haru_icon.png";
        // UI资源 头像
        public const string CharacterIconScene = Root + "/scenes/ui/character_icons/haru_icon.tscn";
        // 角色选择界面资源 背景
        public const string CharacterSelectBg = Root + "/scenes/beijing/beijing.tscn";
        // 角色选择界面资源 选择人物立绘
        public const string CharacterSelectIcon = Root + "/images/charui/char_select_haru.png";
        // 角色选择界面资源 选择人物立绘 锁定状态
        public const string CharacterSelectLockedIcon = Root + "/images/charui/char_select_haru_locked.png";
        // 地图标记
        public const string MapMarker = Root + "/images/packed/map/icons/map_marker_haru.png";
        // 火堆资源 休息时
        public const string RestSiteAnim = Root + "/scenes/rest_site/huodui.tscn";
        // 商店资源 
        public const string MerchantAnim = Root + "/scenes/merchant/shopping.tscn";
        //手臂资源
        public const string ArmPointingTexture = Root + "/images/hands/shizhi.png";
        public const string ArmRockTexture = Root + "/images/hands/shitou.png";
        public const string ArmPaperTexture = Root + "/images/hands/bu.png";
        public const string ArmScissorsTexture = Root + "/images/hands/jiandao.png";


        
        // 默认
        public const string CharacterTransitionSfx = "event:/sfx/ui/wipe_ironclad";
        
        // 能量图标
        public const string TextEnergyIcon = Root + "/images/mp/energy.png";
        public const string BigEnergyIcon = Root + "/images/mp/big/energy_big.png";

        // 卡牌资源路径
        public static string Card(string typeName) => $"{Root}/images/cards/{typeName}.png";
        // 遗物资源路径
        public static string Relic(string typeName) => $"{Root}/images/relics/{typeName}.png";
        // 药水资源路径
        public const string SoulPotionImage = Root + "/images/potions/soul_icon.svg";
    }
}
