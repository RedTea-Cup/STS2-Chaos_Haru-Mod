using Chaos_Haru.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class BengkuiPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/BengkuiDa.png",
            Const.Paths.Root + "/images/powers/BengkuiXiao.png");

    // 每打出1张恐惧减少1层崩溃；归零时移除全部压力并回复10点生命
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner?.Creature != Owner)
            return;

        if (cardPlay.Card is not KongjuCard)
            return;

        if (Amount <= 1)
        {
            // 崩溃归零：先移除压力，再治疗，最后移除自身
            await PowerCmd.Remove<YaliPower>(Owner);
            await CreatureCmd.Heal(Owner, 10m);
            await PowerCmd.Remove(this);
        }
        else
        {
            await PowerCmd.ModifyAmount(this, -1m, null, null);
        }
    }
}
