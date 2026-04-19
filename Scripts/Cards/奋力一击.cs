using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class FengliyijiCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 6;
    // 卡牌类型
    private const CardType type = CardType.Attack;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Rare;
    // 目标类型（AnyEnemy表示任意敌人）
    private const TargetType targetType = TargetType.AnyEnemy;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public FengliyijiCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CalculationBaseVar(10m),  // 基础伤害10点
        new ExtraDamageVar(10m),  // 额外伤害10点
        new CalculatedDamageVar(ValueProp.Move)
            .WithMultiplier((CardModel card, Creature? _) =>
                card.Owner.Creature.Powers
                    .Where(ShouldCountPositiveBuff)
                    .Sum(power => (decimal)power.Amount))
    };

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.CalculatedDamage) // 造成伤害，数值为基础伤害+正面buff层数×额外伤害
            .FromCard(this) // 伤害来源于这张卡牌
            .Targeting(cardPlay.Target) // 伤害目标是玩家选择的目标
            .Execute(choiceContext);
    }

    private static bool ShouldCountPositiveBuff(PowerModel power)
    {
        return power.TypeForCurrentAmount == PowerType.Buff && power.Amount > 0;
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(FengliyijiCard)}.png";
}