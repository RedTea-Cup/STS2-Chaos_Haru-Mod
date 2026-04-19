using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class MaopaoCard : CustomCardModel
{
	private const string _increaseKey = "Increase";

	private decimal _extraDamageFromMaopaoPlays;

	private decimal ExtraDamageFromMaopaoPlays
	{
		get
		{
			return _extraDamageFromMaopaoPlays;
		}
		set
		{
			AssertMutable();
			_extraDamageFromMaopaoPlays = value;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
	{
		new DamageVar(12m, ValueProp.Move),
		new DynamicVar("Increase", 3m)
	};

    // 基础耗能
    private const int energyCost = 2;
    // 卡牌类型
    private const CardType type = CardType.Attack;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Basic;
    // 目标类型（AnyEnemy表示任意敌人）
    private const TargetType targetType = TargetType.AnyEnemy;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public MaopaoCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
			.WithHitVfxNode((Creature t) => NScratchVfx.Create(t, goingRight: true))
			.Execute(choiceContext);
		if (base.Owner?.PlayerCombatState != null)
		{
			IEnumerable<MaopaoCard> enumerable = base.Owner.PlayerCombatState.AllCards.OfType<MaopaoCard>();
			decimal baseValue = base.DynamicVars["Increase"].BaseValue;
			foreach (MaopaoCard item in enumerable)
			{
				item.BuffFromMaopaoPlay(baseValue);
			}
		}
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(6m);
		base.DynamicVars["Increase"].UpgradeValueBy(1m);
	}

	protected override void AfterDowngraded()
	{
		base.AfterDowngraded();
		base.DynamicVars.Damage.BaseValue += ExtraDamageFromMaopaoPlays;
	}

	private void BuffFromMaopaoPlay(decimal extraDamage)
	{
		base.DynamicVars.Damage.BaseValue += extraDamage;
		ExtraDamageFromMaopaoPlays += extraDamage;
	}
	
    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(MaopaoCard)}.png";
}