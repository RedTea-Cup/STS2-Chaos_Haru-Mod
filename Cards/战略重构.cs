using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;

// 战略重构
// 费用: 1
// 效果：每当你打破敌人的格挡时，获得1层充电。
// 升级：耗能-1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ZhanlvechonggouCard()
    : ChaosHaruCardTemplate(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ZhanlvechonggouPower>(1m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<ChongdianPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ZhanlvechonggouPower>(Owner.Creature, DynamicVars["ZhanlvechonggouPower"].BaseValue, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => base.EnergyCost.UpgradeBy(-1);
}