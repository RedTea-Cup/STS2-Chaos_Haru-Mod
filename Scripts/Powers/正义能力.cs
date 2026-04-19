using BaseLibToRitsu.Generated;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Scripts.Powers;

public class ZhengyiPower : CustomPowerModel
{
    private decimal _strengthGainedThisTurn;

    // 类型，Buff或Debuff
    public override PowerType Type => PowerType.Buff;
    // 叠加类型，Counter表示可叠加，Single表示不可叠加
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径。1:1即可。原版游戏大图256x256，小图64x64。
    public override string? CustomPackedIconPath => "res://Chaos_Haru/images/powers/ZhengyiDa.png";
    public override string? CustomBigIconPath => "res://Chaos_Haru/images/powers/ZhengyiXiao.png";

    // 能力的实现：每打出一张自己的攻击牌，获得力量，并在回合结束时扣除本回合获得的力量
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != base.Owner || cardPlay.Card.Type != CardType.Attack)
        {
            return;
        }

        await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, cardPlay.Card);
        _strengthGainedThisTurn += base.Amount;
    }

    //回合结束时扣除力量
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)//回合结束时扣除力量
    {
        if (side != base.Owner.Side || _strengthGainedThisTurn <= 0)
        {
            return;
        }

        decimal strengthToLose = _strengthGainedThisTurn;
        _strengthGainedThisTurn = 0;
        Flash();
        await PowerCmd.Apply<StrengthPower>(base.Owner, -strengthToLose, base.Owner, null);
    }
}