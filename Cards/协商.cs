using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 协商
// 费用: 1
// 效果：获得10点格挡。所有敌人获得2点格挡
// 升级：获得15点格挡。所有敌人获得3点格挡。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class XieshangCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(10m, ValueProp.Move),
        new DynamicVar("EnemyBlock", 2m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay, false);
        if (CombatState == null)
            return;

        foreach (var enemy in CombatState.HittableEnemies)
            await CreatureCmd.GainBlock(enemy, DynamicVars["EnemyBlock"].BaseValue, ValueProp.Unpowered, cardPlay, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(5m);
        DynamicVars["EnemyBlock"].UpgradeValueBy(1m);
    }
}
