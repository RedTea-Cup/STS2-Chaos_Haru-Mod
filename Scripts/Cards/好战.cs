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
public class HaozhanCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 0;
    // 卡牌类型
    private const CardType type = CardType.Skill;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Uncommon;
    // 目标类型
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public HaozhanCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
        
    }

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (!PileType.Hand.GetPile(base.Owner).Cards.Any(card => card.Type == CardType.Attack))
        {
            await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        }
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        base.DynamicVars.Cards.UpgradeValueBy(1m);
    }

    // 卡牌旁出现的提示方框，或预览卡牌。
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [];

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(HaozhanCard)}.png";
}