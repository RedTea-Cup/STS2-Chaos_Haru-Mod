using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 奇袭
// 费用: 0
// 效果：造成5点伤害。有敌人的格挡被打破时，将此牌从弃牌堆移回手牌。
// 升级：造成的伤害增加2点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class QixiCard()
    : ChaosHaruCardTemplate(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
{
    // 基础属性
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(5, ValueProp.Move)
    ];

    // 卡牌标签
    protected override HashSet<CardTag> CanonicalTags => [];

    // 卡牌旁出现的提示方框
    protected override IEnumerable<IHoverTip> AdditionalHoverTips => [
    ];

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, nameof(cardPlay.Target));

        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
            .FromCard(this)
            .Targeting(cardPlay.Target)
            .Execute(choiceContext);
    }

    public override Task AfterBlockBroken(Creature creature)
    {
        return TryReturnToHand(creature);
    }

    private async Task TryReturnToHand(Creature creature)
    {
        if (!creature.IsEnemy || creature.Block > 0)
        {
            return;
        }

        if (base.Pile?.Type == PileType.Discard)
        {
            await CardPileCmd.Add(this, PileType.Hand);
        }
    }

    // 升级效果
    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2); 
    }
}