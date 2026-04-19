using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class MaolianqianyingCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 0;
    // 卡牌类型
    private const CardType type = CardType.Skill;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Common;
    // 目标类型
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public MaolianqianyingCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(base.Owner, nameof(base.Owner));

        var drawPileCards = PileType.Draw.GetPile(base.Owner).Cards
            .Where(card => card is MaopaoCard or Maopao2Card)
            .ToList();

        var discardPileCards = PileType.Discard.GetPile(base.Owner).Cards
            .Where(card => card is MaopaoCard or Maopao2Card)
            .ToList();

        var targetCard = drawPileCards.Concat(discardPileCards).FirstOrDefault();
        if (targetCard != null)
        {
            await CardPileCmd.Add(targetCard, PileType.Hand);
        }

        base.EnergyCost.AddThisCombat(1);
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }

    // 卡牌旁出现的提示方框，或预览卡牌。
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<MaopaoCard>()
    ];

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(MaolianqianyingCard)}.png";
}