using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 自发电实验
// 费用: 1
// 效果：打出后，本回合获得1层充电。此后每回合获得递增的充电。再次打出时，仅额外获得本回合的1层充电。
// 升级：获得固有。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ZifadianshiyanCard()
    : ChaosHaruCardTemplate(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new IntVar("CurrentCharge", 1m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<ChongdianPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ChongdianPower>(Owner.Creature, DynamicVars["CurrentCharge"].BaseValue, Owner.Creature, this, false);

        var hasExistingExperiment = Owner.Creature.Powers.Any(static p => p is ZifadianshiyanPower);
        if (!hasExistingExperiment)
            await PowerCmd.Apply<ZifadianshiyanPower>(Owner.Creature, 1m, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}