using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Scripts.Powers;

public class ZhengyiPower : CustomPowerModel
{
    // 类型，Buff或Debuff
    public override PowerType Type => PowerType.Buff;
    // 叠加类型，Counter表示可叠加，Single表示不可叠加
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径。1:1即可。原版游戏大图256x256，小图64x64。
    public override string? CustomPackedIconPath => "res://Chaos_Haru/images/powers/ZhengyiDa.png";
    public override string? CustomBigIconPath => "res://Chaos_Haru/images/powers/ZhengyiXiao.png";

    // 能力的实现：每打出一张攻击牌，获得一层临时力量
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Type == CardType.Attack)
        {
            await PowerCmd.Apply<FlexPotionPower>(base.Owner, base.Amount, base.Owner, null);
        }
    }
}