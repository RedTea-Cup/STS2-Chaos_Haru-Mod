using System.Linq;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 弹匣
// 费用: 2
// 效果：你的手牌中每有一张攻击牌，就获得1点能量。你在本回合不能再获得能量。
// 升级：费用减少1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class DanxiaCard()
    : ChaosHaruCardTemplate(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    private const string CalculatedEnergyKey = "CalculatedEnergy";

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(0),
        new CalculationBaseVar(0m),
        new CalculationExtraVar(1m),
        new CalculatedVar(CalculatedEnergyKey).WithMultiplier((card, _) =>
            PileType.Hand.GetPile(card.Owner).Cards.Count(static c => c.Type == CardType.Attack))
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [EnergyHoverTip];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(((CalculatedVar)DynamicVars[CalculatedEnergyKey]).Calculate(cardPlay.Target), Owner);
        await PowerCmd.Apply<NoEnergyGainPower>(Owner.Creature, 1m, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}