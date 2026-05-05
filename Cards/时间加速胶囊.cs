using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 时间加速胶囊
// 费用: 0
// 效果：获得3点能量，抽3张牌，获得3层压力。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ShijianjiasujiaonanCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new EnergyVar(3),
        new CardsVar(3),
        new PowerVar<YaliPower>(3m),
    ];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<YaliPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false);
        await PowerCmd.Apply<YaliPower>(Owner.Creature, DynamicVars["YaliPower"].BaseValue, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => DynamicVars["YaliPower"].UpgradeValueBy(-2m);
}