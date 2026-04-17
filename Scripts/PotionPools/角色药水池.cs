using BaseLib.Abstracts;

namespace Chaos_Haru.Scripts.PotionPools;
public class Chaos_HaruPotionPool : CustomPotionPoolModel
{
    // 描述中使用的能量图标。大小为24x24。
    public override string? TextEnergyIconPath => "res://Chaos_Haru/images/mp/energy.png";
    // tooltip和卡牌左上角的能量图标。大小为74x74。
    public override string? BigEnergyIconPath => "res://Chaos_Haru/images/mp/big/energy_big.png";
}