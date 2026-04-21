using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 保持耐心
// 费用: 2
// 效果：获得18点格挡。全体敌人获得你获得的一半的格挡。
// 升级：耗能减少1点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class BaochinaixinCard()
    : ChaosHaruCardTemplate(2, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(18m, ValueProp.Move),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var actualBlockGained = await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, false);
        if (CombatState == null)
            return;

        var enemyBlock = Math.Floor(actualBlockGained / 2m);
        if (enemyBlock < 1m)
            return;

        foreach (var enemy in CombatState.HittableEnemies)
            await CreatureCmd.GainBlock(enemy, enemyBlock, ValueProp.Unpowered, null, false);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}