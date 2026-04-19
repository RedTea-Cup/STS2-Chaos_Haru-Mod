using BaseLibToRitsu.Generated;
using Godot;
using STS2RitsuLib.Scaffolding.Content.Patches;
using STS2RitsuLib.Utils;

namespace Chaos_Haru.Scripts.CardPools;
public class Chaos_HaruCardPool : CustomCardPoolModel, IModCardPoolFrameMaterial
{
    // 卡池的ID。必须唯一防撞车。
    public override string Title => "Chaos_Haru";

    // 独立的能量颜色名，避免覆盖全局 colorless 图标。
    public override string EnergyColorName => "chaos_haru";

    // 描述中使用的能量图标。大小为24x24。
    public override string? TextEnergyIconPath => "res://Chaos_Haru/images/mp/energy.png";
    // tooltip和卡牌左上角的能量图标。大小为74x74。
    public override string? BigEnergyIconPath => "res://Chaos_Haru/images/mp/big/energy_big.png";

    // 卡池的主题色。
    public override Color DeckEntryCardColor => new(0.5f, 0.5f, 1.0f);

    // 如果你使用默认的卡框，可以使用这个颜色来修改卡框的颜色。
    public Material? PoolFrameMaterial => MaterialUtils.CreateHsvShaderMaterial(0.6667f, 0.5216f, 1.0f);

    // 如果你使用自定义卡框图片，重写CustomFrame方法并返回你的卡框图片。
    // public override Texture2D? CustomFrame(CustomCardModel card)
    // {
    //     return card.Type switch
    //     {
    //         CardType.Attack => PreloadManager.Cache.GetAsset<Texture2D>("res://test/images/card_frame_attack.png"),
    //         CardType.Power => PreloadManager.Cache.GetAsset<Texture2D>("res://test/images/card_frame_power.png"),
    //         _ => PreloadManager.Cache.GetAsset<Texture2D>("res://test/images/card_frame_skill.png"),
    //     };
    // }

    // 卡池是否是无色。例如事件、状态等卡池就是无色的。
    public override bool IsColorless => false;
}