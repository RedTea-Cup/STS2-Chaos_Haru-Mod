using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Chaos_Haru.Cards;
// 集结
// 费用: 0
// 效果：抽3张牌。失去1点敏捷。
// 升级：抽牌增加1张。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class JijieCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new CardsVar(3),
        new PowerVar<DexterityPower>(1m),
    ];

    protected override HashSet<CardTag> CanonicalTags => [];

    // 卡牌旁出现的提示方框
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.FromPower<DexterityPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner, false);// 抽牌
        await PowerCmd.Apply<DexterityPower>(Owner.Creature, -DynamicVars.Dexterity.BaseValue, Owner.Creature, this,
            false);
    }

    protected override void OnUpgrade() => DynamicVars.Cards.UpgradeValueBy(1m);
}
