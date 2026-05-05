using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Scaffolding.Content;

namespace Chaos_Haru.Powers;

[RegisterPower]
public sealed class ChongdianPower : ModPowerTemplate
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile =>
        new(Const.Paths.Root + "/images/powers/ChongdianDa.png",
            Const.Paths.Root + "/images/powers/ChongdianXiao.png");

    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Percent", 20m)];
    
    public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (power == this)
            DynamicVars["Percent"].BaseValue = Amount * 20;
        return base.AfterPowerAmountChanged(power, amount, applier, cardSource);
    }

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (dealer != Owner && (dealer == null || !Owner.Pets.Contains(dealer)))
            return 1m;

        if (!ValuePropExtensions.IsPoweredAttack(props))
            return 1m;

        if (cardSource == null || cardSource.Type != CardType.Attack || cardSource.Owner.Creature != Owner)
            return 1m;

        return 1m + Amount * 0.2m;// 每层充电增加20%伤害加成
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Player)
            await PowerCmd.Remove(this);
    }
}
