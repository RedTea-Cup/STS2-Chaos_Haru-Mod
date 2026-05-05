using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Chaos_Haru.Cards;
// 谢幕
// 费用: 1
// 效果：保留。造成3点伤害。当此牌在手牌中，每经过一回合，伤害增加6点。使用后重置。
// 升级：每回合额外伤害改为4点。
[RegisterCard(typeof(ChaosHaruCardPool))]
public sealed class XiemuCard()
    : ChaosHaruCardTemplate(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, true)
{
    private decimal _retainedBonus;
    private bool _skipNextTurnStartBonus = true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(3m, ValueProp.Move),
        new DynamicVar("Increase", 6m),
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .Execute(choiceContext);

        ResetDamage();
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        var owner = Owner;
        if (!ReferenceEquals(owner, player))
            return Task.CompletedTask;

        if (!ReferenceEquals(Pile, PileType.Hand.GetPile(player)))
            return Task.CompletedTask;

        if (_skipNextTurnStartBonus)
        {
            _skipNextTurnStartBonus = false;
            return Task.CompletedTask;
        }

        var increase = DynamicVars["Increase"].BaseValue;
        DynamicVars.Damage.BaseValue += increase;
        _retainedBonus += increase;
        return Task.CompletedTask;
    }
    // 升级后每回合增加的伤害从6点改为9点
    protected override void OnUpgrade() => DynamicVars["Increase"].UpgradeValueBy(3m);

    private void ResetDamage()
    {
        if (_retainedBonus > 0m)
        {
            DynamicVars.Damage.BaseValue -= _retainedBonus;
            _retainedBonus = 0m;
        }

        _skipNextTurnStartBonus = true;
    }
}