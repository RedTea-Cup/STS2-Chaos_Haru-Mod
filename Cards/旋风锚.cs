using Chaos_Haru.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 旋风锚
// 费用: 1
// 效果：对所有敌人造成6点伤害。抽1张牌
// 升级：伤害增加3点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class XuanfengmaoCard()
    : ChaosHaruCardTemplate(1, CardType.Attack, CardRarity.Common, TargetType.RandomEnemy, true)
{
    // 注册关键词
    protected override IEnumerable<string> RegisteredKeywordIds => [FensuiRules.FensuiKeywordId];
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(6m, ValueProp.Move),
        new CardsVar(1),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).TargetingAllOpponents(CombatState!)
            .WithAttackerAnim("Cast", 0.5f, null)
            .BeforeDamage(async () =>
            {
                var targets = CombatState!.HittableEnemies.ToList();
                var sweep = NSweepingBeamVfx.Create(Owner.Creature, targets);
                if (sweep is null)
                    return;

                var room = NCombatRoom.Instance;
                if (room != null)
                    GodotTreeExtensions.AddChildSafely(room.CombatVfxContainer, sweep);

                await Cmd.Wait(0.5f, false);
            })
            .Execute(choiceContext);

        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (!ReferenceEquals(cardSource, this))
            return 1m;

        return FensuiRules.GetDamageMultiplier(target, props, cardSource);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3m);
}
