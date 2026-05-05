using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Chaos_Haru.Cards;

// 毁灭
// 费用: 1
// 效果：回合开始时获得1点压力，抽1张牌。
// 升级：耗能-1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class HuimieCard()
    : ChaosHaruCardTemplate(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<YaliPower>()
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<HuimiePower>(Owner.Creature, 1m, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => base.EnergyCost.UpgradeBy(-1);
}