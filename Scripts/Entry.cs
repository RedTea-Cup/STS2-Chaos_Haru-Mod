using Chaos_Haru.Scripts.Cards;
using Chaos_Haru.Scripts.Relics;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using STS2RitsuLib;

namespace Chaos_Haru.Scripts;

// 必须要加的属性，用于注册Mod。字符串和初始化函数命名一致。
[ModInitializer("Init")]
public class Entry
{
    // 初始化函数
    public static void Init()
    {
        // 打patch（即修改游戏代码的功能）用
        // 传入参数随意，只要不和其他人撞车即可
        var harmony = new Harmony("sts2.reme.chaos_haru");
        harmony.PatchAll();
        // 使得tscn可以加载自定义脚本
        ScriptManagerBridge.LookupScriptsInAssembly(typeof(Entry).Assembly);

        // RitsuLib 下需要显式注册先古牙齿/奥罗巴斯之触的映射关系
        RitsuLibFramework.RegisterArchaicToothTranscendenceMapping<MaopaoCard, Maopao2Card>();
        RitsuLibFramework.RegisterTouchOfOrobasRefinementMapping<HaruRelic, Haru2Relic>();

        Log.Info("Mod initialized!");
    }
}



