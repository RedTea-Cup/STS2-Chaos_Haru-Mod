using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace Chaos_Haru.Cards;
// 充电
// 费用: 1
// 效果：根据当前手牌中的攻击牌数量，获得等量的充电。
// 升级：获得保留。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class ChongdianCard()
    : ChaosHaruCardTemplate(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [HoverTipFactory.FromPower<ChongdianPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        var attackCount = PileTypeExtensions.GetPile(PileType.Hand, Owner).Cards.Count(static c => c.Type == CardType.Attack);
        if (attackCount > 0)
            await PowerCmd.Apply<ChongdianPower>(Owner.Creature, attackCount, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}
