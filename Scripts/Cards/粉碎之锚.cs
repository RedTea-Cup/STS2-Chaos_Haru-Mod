using BaseLibToRitsu.Generated;
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
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(8, ValueProp.Move)
    ];

    public FensuidajiCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 添加关键词
    public override IEnumerable<CardKeyword> CanonicalKeywords => [FensuiKeywords.Fensui];
    
    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (cardSource != this)
        {
            return 1m;
        }

        return FensuiKeywords.GetDamageMultiplier(target, props, cardSource);
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2); 
        AddKeyword(CardKeyword.Retain);
    }

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(FensuidajiCard)}.png";
}