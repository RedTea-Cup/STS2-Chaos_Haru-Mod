using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 能量涌动
// 费用: 0
// 效果：获得1点能量。下回合获得1点能量。
// 升级：获得2点能量。下回合获得1点能量。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class NengliangyongdongCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(1),
        new PowerVar<EnergyNextTurnPower>(1m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [EnergyHoverTip];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        await PowerCmd.Apply<EnergyNextTurnPower>(Owner.Creature, DynamicVars["EnergyNextTurnPower"].BaseValue,
            Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => DynamicVars.Energy.UpgradeValueBy(1m);
}
