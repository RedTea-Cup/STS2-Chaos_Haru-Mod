using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Character;

public sealed class ChaosHaruPotionPool : TypeListPotionPoolModel
{
    public override string EnergyColorName => Const.EnergyColorName; //能量颜色名称

    public override string? TextEnergyIconPath => Const.Paths.TextEnergyIcon; //文本能量图标路径

    public override string? BigEnergyIconPath => Const.Paths.BigEnergyIcon; //大能量图标路径
}
