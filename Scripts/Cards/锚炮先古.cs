using BaseLib.Abstracts;
using BaseLib.Utils;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Scripts.Cards;

[Pool(typeof(Chaos_HaruCardPool))]

public class Maopao2Card : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 2;
    // 卡牌类型
    private const CardType type = CardType.Attack;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Ancient;
    // 目标类型（AnyEnemy表示任意敌人）
    private const TargetType targetType = TargetType.AnyEnemy;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public Maopao2Card() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    private const string _increaseKey = "Increase";

	private decimal _extraDamageFromMaopaoPlays;

	private decimal ExtraDamageFromMaopao2Plays
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
		new DamageVar(24m, ValueProp.Move),
		new DynamicVar("Increase", 6m)
	};

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
			.WithHitVfxNode((Creature t) => NScratchVfx.Create(t, goingRight: true))
			.Execute(choiceContext);
		if (base.Owner?.PlayerCombatState != null)
		{
			IEnumerable<Maopao2Card> enumerable = base.Owner.PlayerCombatState.AllCards.OfType<Maopao2Card>();
			decimal baseValue = base.DynamicVars["Increase"].BaseValue;
			foreach (Maopao2Card item in enumerable)
			{
				item.BuffFromMaopao2Play(baseValue);
			}
		}
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(12m);
		base.DynamicVars["Increase"].UpgradeValueBy(2m);
	}

	protected override void AfterDowngraded()
	{
		base.AfterDowngraded();
		base.DynamicVars.Damage.BaseValue += ExtraDamageFromMaopao2Plays;
	}

	private void BuffFromMaopao2Play(decimal extraDamage)
	{
		base.DynamicVars.Damage.BaseValue += extraDamage;
		ExtraDamageFromMaopao2Plays += extraDamage;
	}

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(Maopao2Card)}.png";

}
