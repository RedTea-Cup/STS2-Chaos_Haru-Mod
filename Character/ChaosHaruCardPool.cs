using Godot;
using STS2RitsuLib.Scaffolding.Content;
using STS2RitsuLib.Utils;

namespace Chaos_Haru.Character;

public sealed class ChaosHaruCardPool : TypeListCardPoolModel
{
    public override string Title => "Chaos_Haru"; //卡池的标题

    public override string EnergyColorName => Const.EnergyColorName; //能量颜色的名称

    public override string? TextEnergyIconPath => Const.Paths.TextEnergyIcon; //文本能量图标的路径

    public override string? BigEnergyIconPath => Const.Paths.BigEnergyIcon; //大能量图标的路径

    public override Color DeckEntryCardColor => new(0.5f, 0.5f, 1f, 1f); //牌组界面卡牌的颜色

    public override Material? PoolFrameMaterial => MaterialUtils.CreateHsvShaderMaterial(0.6667f, 0.5216f, 1f); //卡框的颜色

    public override bool IsColorless => false; //是否是无色卡池（如果是无色卡池，角色将无法获得该卡池中的牌）
}
