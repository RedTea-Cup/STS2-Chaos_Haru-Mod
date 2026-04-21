using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 锚链牵引
// 费用: 0
// 效果：将抽牌堆或弃牌堆中的一张锚炮牌免费打出。本卡牌能耗增加1点。
// 升级：添加保留。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class MaolianqianyingCard()
    : ChaosHaruCardTemplate(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.FromCard<MaopaoCard>(false)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(Owner);
        var draw = PileTypeExtensions.GetPile(PileType.Draw, Owner).Cards.Where(static c => c is MaopaoCard or Maopao2Card).ToList();
        var discard = PileTypeExtensions.GetPile(PileType.Discard, Owner).Cards.Where(static c => c is MaopaoCard or Maopao2Card).ToList();
        var targetCard = draw.Concat(discard).FirstOrDefault();
        if (targetCard != null)
            await CardCmd.AutoPlay(choiceContext, targetCard, null);

        EnergyCost.AddThisCombat(1, false);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}
