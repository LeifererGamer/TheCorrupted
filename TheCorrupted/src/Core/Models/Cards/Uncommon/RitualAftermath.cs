using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Commands;
using TheCorrupted.TheCorrupted.src.Core.Models.Extensions;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Uncommon
{
internal class RitualAftermath() : CorruptedCardModel<WeakPower>(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new CorruptedVar(2),

            new CalculationBaseVar(5m),
            new ExtraDamageVar(3m),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(delegate(CardModel card, Creature? _)
            {
                CardModel card2 = card;
                return CombatManager.Instance.History.Entries.OfType<CardExhaustedEntry>().Count((CardExhaustedEntry e) => e.HappenedThisTurn(card2.CombatState) && e.Actor == card2.Owner.Creature);
            }),
            new CalculationExtraVar(3m),
            new CalculatedVar("Half").WithMultiplier(delegate(CardModel card, Creature? _)
            {
                CardModel card2 = card;
                return (CombatManager.Instance.History.Entries.OfType<CardExhaustedEntry>().Count((CardExhaustedEntry e) => e.HappenedThisTurn(card2.CombatState) && e.Actor == card2.Owner.Creature) / 2);
            }),

        ];

        public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2m);
        }

        protected override async Task DoOnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            decimal amount = cardPlay.IsAutoPlay ? (DynamicVars.CalculatedDamage.IntValue / 2) : DynamicVars.CalculatedDamage.IntValue;

            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(amount).FromCard(this).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
                .Execute(choiceContext);
        }
    }
}


