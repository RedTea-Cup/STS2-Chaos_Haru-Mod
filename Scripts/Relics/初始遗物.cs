using BaseLib.Abstracts;
using BaseLib.Utils;
using Chaos_Haru.Scripts.RelicPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Chaos_Haru.Scripts.Relics;

// 加入哪个遗物池
[Pool(typeof(Chaos_HaruRelicPool))]
public class HaruRelic : CustomRelicModel
{
    // 稀有度
    public override RelicRarity Rarity => RelicRarity.Starter;

    // 遗物的数值。替换本地化中的{Cards}。
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];

    // 小图标（原版85x85）
    public override string PackedIconPath => $"res://Chaos_Haru/images/relics/{nameof(HaruRelic)}.png";
    // 轮廓图标（原版85x85）
    protected override string PackedIconOutlinePath => $"res://Chaos_Haru/images/relics/{nameof(HaruRelic)}.png";
    // 大图标（原版256x256）
    protected override string BigIconPath => $"res://Chaos_Haru/images/relics/{nameof(HaruRelic)}.png";

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        // 这里的DynamicVars.Cards.IntValue为上面设置的CardsVar的数值。
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.IntValue, player);
    }

    // 初始遗物的升级可以写这里
    public override RelicModel? GetUpgradeReplacement() => ModelDb.Relic<Haru2Relic>();
}