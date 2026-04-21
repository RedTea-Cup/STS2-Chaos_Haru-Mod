using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 快速拔枪
// 费用: 0
// 效果：抽1张牌。若抽到的牌是攻击牌，获得2点力量，否则失去1点力量。
// 升级：获得的力量增加1点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class KuaisubaqiangCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    // 基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(1),
        new PowerVar<StrengthPower>(2m)
    ];

    // 卡牌标签
    protected override HashSet<CardTag> CanonicalTags => [];

    // 卡牌旁出现的提示方框
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var drawnCard = (await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false)).FirstOrDefault();

        if (drawnCard != null && drawnCard.Type == CardType.Attack)
            await PowerCmd.Apply<StrengthPower>(Owner.Creature, DynamicVars.Strength.BaseValue, Owner.Creature, this, false);
        else
            await PowerCmd.Apply<StrengthPower>(Owner.Creature, -1m, Owner.Creature, this, false);
    }
    
    // 升级效果
    protected override void OnUpgrade()
    {
        DynamicVars.Strength.UpgradeValueBy(1m);
    }
}