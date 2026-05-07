using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards
{
    internal abstract class CorruptedCardModel<TPower>(int cost, CardType type, CardRarity rarity, TargetType target)
        : TheCorruptedCardModel(cost, type, rarity, target)
     where TPower : PowerModel
    {

        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != Owner.Creature.Side) return;

            if (Pile.Type.Equals(PileType.Hand))
            {
                await CardCmd.AutoPlay(choiceContext, this, null);

                // Hier nutzen wir jetzt den Platzhalter <TPower> statt <WeakPower>
                await PowerCmd.Apply<TPower>(Owner.Creature, DynamicVars["Corrupted"].BaseValue, Owner.Creature, this);

                IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(
                    Owner,
                    from c in ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(Owner.UnlockState, Owner.RunState.CardMultiplayerConstraint)
                    where c.Type == CardType.Curse && (c is not Enthralled || Owner.Relics.Any(r => r is BlueCandle))
                    select c,
                    DynamicVars["Corrupted"].IntValue,
                    Owner.RunState.Rng.CombatCardGeneration
                );
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            }
        }
    }
}
