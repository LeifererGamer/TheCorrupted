using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Afflictions;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Curse;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Rare;
using TheCorrupted.TheCorrupted.src.Core.Models.Enchantments;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Relics
{

    internal class BlueCandle : RelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromKeyword(CardKeyword.Unplayable),
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        ];

        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
        {
            if (player == Owner && combatState.RoundNumber == 1)
            {
                IEnumerable<CardModel> cards = base.Owner.PlayerCombatState.AllCards.Where((CardModel c) => c.Type == CardType.Curse || (c.Type == CardType.Status && c.Owner.Creature.HasPower<StatusQuoPower>() && c is not FranticEscape));
                foreach (CardModel card in cards)
                {
                    await SetAffliction(card);
                }
            }
        }

        public override async Task AfterCardEnteredCombat(CardModel card)
        {
            if (card.Owner == Owner && (card.Type == CardType.Curse || (card.Type == CardType.Status && card.Owner.Creature.HasPower<StatusQuoPower>() && card is not FranticEscape)))
            {
                await SetAffliction(card);
            }
        }

        public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner == base.Owner && (cardPlay.Card.Type == CardType.Curse || (cardPlay.Card.Type == CardType.Status && cardPlay.Card.Owner.Creature.HasPower<StatusQuoPower>()) && cardPlay.Card is not FranticEscape) && !cardPlay.IsAutoPlay)
            {
                var damage = 1m;
                if (cardPlay.Card is Greed)
                    damage = 4m;
                await CreatureCmd.Damage(context, Owner.Creature, damage, ValueProp.Unblockable, cardPlay.Card);
            }
        }

        private async Task SetAffliction(CardModel card)
        {
            card.EnergyCost.SetThisCombat(-1, reduceOnly: true);
            card.RemoveKeyword(CardKeyword.Unplayable);
            card.AddKeyword(CardKeyword.Exhaust);
            if (card is AscendersBane)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is BadLuck)
            {
                await CardCmd.Afflict<BadLuckAff>(card, 1m);
            }
            else if (card is Clumsy)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is CurseOfTheBell)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Debt)
            {
                await CardCmd.Afflict<DebtAff>(card, 1m);
            }
            else if (card is Decay)
            {
                await CardCmd.Afflict<DecayAff>(card, 1m);
            }
            else if (card is Doubt)
            {
                await CardCmd.Afflict<DoubtAff>(card, 1m);
            }
            else if (card is Enthralled)
            {
                await CardCmd.Afflict<EnthralledAff>(card, 1m);
            }
            else if (card is Folly)
            {
                await CardCmd.Afflict<FollyAff>(card, 1m);
            }
            else if (card is Greed)
            {
                await CardCmd.Afflict<GreedAff>(card, 1m);
            }
            else if (card is Guilty)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Injury)
            {
                await CardCmd.Afflict<InjuryAff>(card, 1m);
            }
            else if (card is Normality)
            {
                await CardCmd.Afflict<NormalityAff>(card, 1m);
            }
            else if (card is PoorSleep)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Regret)
            {
                await CardCmd.Afflict<RegretAff>(card, 1m);
            }
            else if (card is Shame)
            {
                await CardCmd.Afflict<ShameAff>(card, 1m);
            }
            else if (card is SporeMind)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Writhe)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is CorruptionCorrupted)
            {
                await CardCmd.Afflict<CorruptionCorruptedAff>(card, 1m);
            }
            else if (card is Necronomicurse)
            {
                await CardCmd.Afflict<NecronomicurseAff>(card, 1m);
            }
            else if (card is SpreadingCorruption)
            {
                await CardCmd.Afflict<SpreadingCorruptionAff>(card, 1m);
            }
            else if (card is Beckon)
            {
                await CardCmd.Afflict<BeckonAff>(card, 1m);
            }
            else if (card is Burn)
            {
                await CardCmd.Afflict<BurnAff>(card, 1m);
            }
            else if (card is Dazed)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Infection)
            {
                await CardCmd.Afflict<InfectionAff>(card, 1m);
            }
            else if (card is Slimed)
            {
                await CardCmd.Afflict<SlimedAff>(card, 1m);
            }
            else if (card is Soot)
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
            else if (card is Toxic)
            {
                await CardCmd.Afflict<ToxicAff>(card, 1m);
            }
            else if (card is MegaCrit.Sts2.Core.Models.Cards.Void)
            {
                await CardCmd.Afflict<VoidAff>(card, 1m);
            }
            else if (card is Wound)
            {
                await CardCmd.Afflict<WoundAff>(card, 1m);
            }
            else
            {
                await CardCmd.Afflict<BlueCandleAff>(card, 1m);
            }
        }
    }
}