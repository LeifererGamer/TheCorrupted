using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{

internal class CorruptedDeliveryPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
        {
            if (card.Owner.Creature != base.Owner)
            {
                return;
            }
            if (card.Type == CardType.Curse || (card.Type == CardType.Status && card.Owner.Creature.HasPower<StatusQuoPower>()))
            {
                List<CardModel> newCards = CardFactory.GetDistinctForCombat(base.Owner.Player, from c in base.Owner.Player.Character.CardPool.GetUnlockedCards(base.Owner.Player.UnlockState, base.Owner.Player.RunState.CardMultiplayerConstraint)
                                                                                        where c.Rarity == CardRarity.Rare || c.Rarity == CardRarity.Uncommon
                                                                                        select c, base.Amount, base.Owner.Player.RunState.Rng.CombatCardGeneration).ToList();
                foreach (CardModel newCard in newCards)
                    await CardPileCmd.AddGeneratedCardToCombat(newCard, PileType.Hand, Owner.Player);
            }

        }
    }
}