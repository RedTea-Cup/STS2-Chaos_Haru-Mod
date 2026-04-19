using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using Chaos_Haru.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class ZhengyixintiaoCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 2;
    // 卡牌类型
    private const CardType type = CardType.Skill;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Rare;
    // 目标类型（Self表示自身）
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public ZhengyixintiaoCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 卡牌的关键词
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ZhengyiPower>(1m)
    ];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<ZhengyiPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        RemoveKeyword(CardKeyword.Ethereal);
    }

    // 卡牌旁出现的提示方框，或预览卡牌。
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ZhengyiPower>(),
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(ZhengyixintiaoCard)}.png";
}