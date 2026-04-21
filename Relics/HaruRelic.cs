using Chaos_Haru.Cards;
using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace Chaos_Haru.Relics;

[RegisterRelic(typeof(ChaosHaruRelicPool))]
[RegisterCharacterStarterRelic(typeof(ChaosHaruCharacter))]
[RegisterTouchOfOrobasRefinement(typeof(Haru2Relic))]
public sealed class HaruRelic : ChaosHaruRelicTemplate
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    protected override IEnumerable<IHoverTip> AdditionalHoverTips =>
    [
        HoverTipFactory.FromPower<YaliPower>(),
        HoverTipFactory.FromPower<BengkuiPower>(),
        HoverTipFactory.FromCard<KongjuCard>(),
    ];

    //  每场战斗中，回合开始时增加1点压力。
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
            return;

        Flash();
        await PowerCmd.Apply<YaliPower>(Owner.Creature, 1m, Owner.Creature, null, false);
    }
}

