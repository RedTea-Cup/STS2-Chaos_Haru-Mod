using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 高速连射
// 费用: 1
// 效果：对一个敌人造成5点伤害两次，抽2张牌。
// 升级：抽牌增加1张。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class GaosuliansheCard()
	: ChaosHaruCardTemplate(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy, true)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(5m, ValueProp.Move),
        new CardsVar(2)
    ];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target);
		await DamageCmd.Attack(DynamicVars.Damage.BaseValue)
			.WithHitCount(2)
			.FromCard(this)
			.Targeting(cardPlay.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false);
	}

	protected override void OnUpgrade() => DynamicVars.Cards.UpgradeValueBy(1m);// 升级：抽牌增加1张。
}
