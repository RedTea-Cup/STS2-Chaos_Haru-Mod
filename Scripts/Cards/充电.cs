using BaseLibToRitsu.Generated;
using Chaos_Haru.Scripts.CardPools;
using Chaos_Haru.Scripts.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Scripts.Cards;

// 加入哪个卡池
[Pool(typeof(Chaos_HaruCardPool))]
public class ChongdianCard : CustomCardModel
{
    // 基础耗能
    private const int energyCost = 1;
    // 卡牌类型
    private const CardType type = CardType.Skill;
    // 卡牌稀有度
    private const CardRarity rarity = CardRarity.Uncommon;
    // 目标类型
    private const TargetType targetType = TargetType.Self;
    // 是否在卡牌图鉴中显示
    private const bool shouldShowInCardLibrary = true;

    public ChongdianCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    // 卡牌的基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);

        int attackCount = PileType.Hand.GetPile(base.Owner).Cards.Count(card => card.Type == CardType.Attack);
        if (attackCount <= 0)
        {
            return;
        }

        await PowerCmd.Apply<ChongdianPower>(base.Owner.Creature, attackCount, base.Owner.Creature, this);
    }

    // 升级后的效果逻辑
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }

    // 卡牌旁出现的提示方框，或预览卡牌。
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChongdianPower>(),
    ];

    // 卡面路径
    public override string PortraitPath => $"res://Chaos_Haru/images/cards/{nameof(ChongdianCard)}.png";
}