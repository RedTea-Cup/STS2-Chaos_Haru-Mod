using Chaos_Haru.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace Chaos_Haru.Relics;

[RegisterRelic(typeof(ChaosHaruRelicPool))]
public sealed class Haru2Relic : ChaosHaruRelicTemplate
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    //  每场战斗中，回合开始时减少1点压力。
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
            return;

        Flash();
        await PowerCmd.Apply<YaliPower>(Owner.Creature, -1m, Owner.Creature, null, false);
    }
}