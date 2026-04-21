using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 防御
// 费用: 1
// 效果：获得5点格挡。
// 升级：格挡增加3点。
[RegisterCard(typeof(ChaosHaruCardPool))]
[RegisterCharacterStarterCard(typeof(ChaosHaruCharacter), 4)]
public sealed class DefendCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Basic, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5m, ValueProp.Move)];

    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) =>
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, false);

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3m);
}
