using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.src.Core.Models.Cards
{
    internal abstract class CorruptedCardModel<TPower>(int cost, CardType type, CardRarity rarity, TargetType target)
        : TheCorruptedCardModel(cost, type, rarity, target)
     where TPower : PowerModel
    {

        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != base.Owner.Creature.Side) return;

            if (this.Pile.Type.Equals(PileType.Hand))
            {
                await CardCmd.AutoPlay(choiceContext, this, null);

                // Hier nutzen wir jetzt den Platzhalter <TPower> statt <WeakPower>
                await PowerCmd.Apply<TPower>(base.Owner.Creature, base.DynamicVars["Corrupted"].BaseValue, base.Owner.Creature, this);

                IEnumerable<CardModel> curses = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<CurseCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.CombatState.RunState.CardMultiplayerConstraint), base.DynamicVars["Corrupted"].IntValue, base.CombatState.RunState.Rng.CombatCardGeneration);
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, true, CardPilePosition.Random));
            }
        }
    }
}
