using BaseLib.Abstracts;
using BaseLib.Utils;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Scripts.Cards;

[Pool(typeof(Chaos_HaruCardPool))]
public class MaodianCard : CustomCardModel
{   
    // 基础耗能
    private const int energyCost = 0;
    // 卡牌类型
    private const CardType type = CardType.Skill;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Basic;
    // 目标类型
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new EnergyVar(0)
    };

    public MaodianCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 效果的实现
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(base.Owner, "base.Owner");

        // 获得能量
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);

        // 从抽牌堆和弃牌堆中搜索锚炮卡牌
        var drawPileCards = PileType.Draw.GetPile(base.Owner).Cards.OfType<MaopaoCard>().ToList();
        var discardPileCards = PileType.Discard.GetPile(base.Owner).Cards.OfType<MaopaoCard>().ToList();
        // 从抽牌堆和弃牌堆中搜索神·锚炮卡牌
        var drawPile2Cards = PileType.Draw.GetPile(base.Owner).Cards.OfType<Maopao2Card>().ToList();
        var discardPile2Cards = PileType.Discard.GetPile(base.Owner).Cards.OfType<Maopao2Card>().ToList();

        // 合并两个堆中的结果
        var allMaopaoCards = drawPileCards.Concat(discardPileCards).ToList();
        var allMaopao2Cards = drawPile2Cards.Concat(discardPile2Cards).ToList();

        if (allMaopaoCards.Count > 0)
        {
            // 将第一张锚炮移至手牌
            MaopaoCard targetCard = allMaopaoCards[0];
            await CardPileCmd.Add(targetCard, PileType.Hand);
        }

        if (allMaopao2Cards.Count > 0)
        {
            // 将第一张神·锚炮移至手牌
            Maopao2Card targetCard = allMaopao2Cards[0];
            await CardPileCmd.Add(targetCard, PileType.Hand);
        }
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        base.DynamicVars.Energy.UpgradeValueBy(1m);
    }

    // 通过HoverTipFactory添加各种提示文本
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<MaopaoCard>()
    ];
    
    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(MaodianCard)}.png";
}