using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 护佑
// 费用: 1
// 效果：获得8点格挡。如果你没有格挡，则获得双倍。
// 升级：格挡增加3点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class HuyouCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(8m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var blockAmount = DynamicVars.Block.BaseValue;
        if (Owner.Creature.Block <= 0)
            blockAmount *= 2m;

        await CreatureCmd.GainBlock(Owner.Creature, blockAmount, DynamicVars.Block.Props, cardPlay, false);
    }

    protected override void OnUpgrade() => DynamicVars.Block.UpgradeValueBy(3m);
}
