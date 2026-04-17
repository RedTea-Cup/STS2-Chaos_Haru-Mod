using BaseLib.Abstracts;
using BaseLib.Utils;
using Chaos_Haru.Scripts.CardPools;
using Chaos_Haru.Scripts.Keywords;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class FensuidajiCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 1;
    // 卡牌类型
    private const CardType type = CardType.Attack;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Common;
    // 目标类型（AnyEnemy表示任意敌人）
    private const TargetType targetType = TargetType.AnyEnemy;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new CalculationBaseVar(10m),  // 基础伤害10点
        new ExtraDamageVar(10m),  // 额外伤害10点
        new CalculatedDamageVar(ValueProp.Move)
            .WithMultiplier((CardModel _, Creature? target) =>
                target?.Block >= 1 ? 1 : 0)  // 有格挡时乘以1（加上额外伤害），无格挡时乘以0（只有基础伤害）
    };

    public FensuidajiCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 添加关键词
    public override IEnumerable<CardKeyword> CanonicalKeywords => [FensuiKeywords.Fensui];
    
    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(FensuidajiCard)}.png";
}