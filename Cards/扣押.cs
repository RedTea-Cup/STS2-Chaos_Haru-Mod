using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Chaos_Haru.Cards;

// 扣押
// 费用: 1
// 效果：每回合第一次打出锚炮（包括神·锚炮）时，将其从弃牌堆移回手牌。
// 升级：耗能-1。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class KouyaCard()
    : ChaosHaruCardTemplate(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
{
    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromCard<MaopaoCard>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<KouyaPower>(Owner.Creature, 1m, Owner.Creature, this, false);
    }

    protected override void OnUpgrade() => base.EnergyCost.UpgradeBy(-1);
}