using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 寂界虹芒
// 费用: X
// 效果：X-1点无实体。
// 升级：X点无实体。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class JijiehongmangCard()
    : ChaosHaruCardTemplate(0, CardType.Power, CardRarity.Ancient, TargetType.Self, true)
{
    // X费用标记
    protected override bool HasEnergyCostX => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new IntVar("IntangibleOffset", -1m)];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.FromPower<IntangiblePower>()];

    protected override HashSet<CardTag> CanonicalTags => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var xValue = ResolveEnergyXValue();
        var intangibleAmount = Math.Max(0, xValue + (int)DynamicVars["IntangibleOffset"].BaseValue);
        if (intangibleAmount > 0)
            await PowerCmd.Apply<IntangiblePower>(Owner.Creature, intangibleAmount, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => DynamicVars["IntangibleOffset"].UpgradeValueBy(1m);
}
