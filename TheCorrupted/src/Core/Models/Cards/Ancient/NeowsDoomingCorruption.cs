using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Token;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient
{

internal class NeowsDoomingCorruption() : CardModel(1, CardType.Skill, CardRarity.Ancient, TargetType.AllEnemies), ICustomModel
    {
        public override CardPoolModel Pool => ModelDb.CardPool<CorruptedCardPool>();

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>(),
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromCard<SpreadingCorruption>()
];

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new PowerVar<WeakPower>(3),
            new PowerVar<VulnerablePower>(3),
            new CalculationBaseVar(0m),
            new CalculationExtraVar(0.5m),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier(static (card, _) => card.Owner.Creature.HasPower<DoomPower>() ? card.Owner.Creature.GetPower<DoomPower>().Amount : 0),

            new ExtraDamageVar(5m),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier((CardModel card, Creature? _) => PileType.Exhaust.GetPile(card.Owner).Cards.Count((CardModel c) => c.Type == CardType.Curse || c.Type == CardType.Status && c.Owner.Creature.HasPower<StatusQuoPower>())),
            new PowerVar<DoomPower>(10),
            new RitualVar(1),
        ];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [
            CardKeyword.Exhaust,
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DoomPower>([Owner.Creature], DynamicVars.Doom.BaseValue, Owner.Creature, this);
            await SpreadingCorruption.CreateInHand(Owner, DynamicVars["Ritual"].IntValue, CombatState);
            var ritualPerformed = await Ritual.ChooseIfPerformRitual(choiceContext, Owner, this, async (card) =>
            {
                await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this).TargetingAllOpponents(CombatState)
                .WithHitFx("vfx/vfx_giant_horizontal_slash")
                .SpawningHitVfxOnEachCreature()
                    .Execute(choiceContext);
                await PowerCmd.Apply<VulnerablePower>(CombatState.HittableEnemies, DynamicVars.Vulnerable.BaseValue, Owner.Creature, this);
            });
            if (!ritualPerformed)
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.CalculatedBlock.Calculate(cardPlay.Target), DynamicVars.CalculatedBlock.Props, cardPlay);
                await PowerCmd.Apply<WeakPower>(CombatState.HittableEnemies, DynamicVars.Weak.BaseValue, Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.ExtraDamage.UpgradeValueBy(1m);
            DynamicVars.CalculationExtra.UpgradeValueBy(1m);
            DynamicVars.Doom.UpgradeValueBy(5m);
            DynamicVars["Ritual"].UpgradeValueBy(1m);
        }

    }

}