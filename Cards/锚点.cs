using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 锚点
// 费用: 0
// 效果：将一张锚炮牌从牌堆移到手牌。
// 升级：获得1点能量。
[RegisterCard(typeof(ChaosHaruCardPool))]
[RegisterCharacterStarterCard(typeof(ChaosHaruCharacter), 1)]
public sealed class MaodianCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(0)];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.FromCard<MaopaoCard>(false)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(Owner);
        await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);

        var draw = PileTypeExtensions.GetPile(PileType.Draw, Owner).Cards.Where(static c => c is MaopaoCard or Maopao2Card).ToList();
        var discard = PileTypeExtensions.GetPile(PileType.Discard, Owner).Cards.Where(static c => c is MaopaoCard or Maopao2Card).ToList();
        var targetCard = draw.Concat(discard).FirstOrDefault();
        if (targetCard != null)
            await CardPileCmd.Add(targetCard, PileType.Hand, CardPilePosition.Bottom, null, false);
    }

    protected override void OnUpgrade() => DynamicVars.Energy.UpgradeValueBy(1);
}
