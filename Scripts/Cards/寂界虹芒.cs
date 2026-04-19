using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class JijiehongmangCard : CustomCardModel
{
    protected override bool HasEnergyCostX => true;

    // 基础耗能j
    private const int energyCost = 0;
    // 卡牌类型
    private const CardType type = CardType.Power;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Ancient;
    // 目标类型
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new IntVar("IntangibleOffset", -1m)
    ];

    // 原版关键词提示
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<IntangiblePower>()
    ];

    public JijiehongmangCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        int xValue = ResolveEnergyXValue();
        int intangibleAmount = Math.Max(0, xValue + (int)base.DynamicVars["IntangibleOffset"].BaseValue);
        if (intangibleAmount > 0)
        {
            await PowerCmd.Apply<IntangiblePower>(base.Owner.Creature, intangibleAmount, base.Owner.Creature, this);
        }
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        base.DynamicVars["IntangibleOffset"].UpgradeValueBy(1m);
    }

    // 卡牌标签
    protected override HashSet<CardTag> CanonicalTags => [];

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(JijiehongmangCard)}.png";
}