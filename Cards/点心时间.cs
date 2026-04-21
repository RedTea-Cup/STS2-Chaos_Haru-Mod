using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 点心时间
// 费用: 0
// 效果：你和一名敌人各获得3点格挡。
// 升级：获得固有。
[RegisterCard(typeof(ChaosHaruCardPool))]
[RegisterCharacterStarterCard(typeof(ChaosHaruCharacter), 1)]
public sealed class DianxinshijianCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(3m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, false);
        await CreatureCmd.GainBlock(cardPlay.Target, DynamicVars.Block.BaseValue, ValueProp.Unpowered, cardPlay, false);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}