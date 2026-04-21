using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 忏悔
// 费用: 1
// 效果：恢复3点生命。所有敌人获得10点格挡
// 升级：恢复5点生命。所有敌人获得10点格挡。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ChanhuiCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new HealVar(3m),
        new DynamicVar("EnemyBlock", 10m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.Heal(Owner.Creature, DynamicVars.Heal.BaseValue);
        if (CombatState == null)
            return;

        foreach (var enemy in CombatState.HittableEnemies)
            await CreatureCmd.GainBlock(enemy, DynamicVars["EnemyBlock"].BaseValue, ValueProp.Unpowered, cardPlay, false);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Heal.UpgradeValueBy(2m);
    }
}
