using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.TheCorrupted.src.Core.Models.CardPools;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards
{
    internal abstract class CorruptedCardModel<TPower>(int cost, CardType type, CardRarity rarity, TargetType target)
        : TheCorruptedCardModel(cost, type, rarity, target)
     where TPower : PowerModel
    {

        public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (!participants.ToList().Contains(Owner.Creature)) return;

            if (Pile.Type.Equals(PileType.Hand))
            {
                await CardCmd.AutoPlay(choiceContext, this, null);

                // Hier nutzen wir jetzt den Platzhalter <TPower> statt <WeakPower>
                await PowerCmd.Apply<TPower>(choiceContext, Owner.Creature, DynamicVars["Corrupted"].BaseValue, Owner.Creature, this);

                IEnumerable<CardModel> curses = CorruptedCardPool.GetRandomCurses(Owner, DynamicVars["Corrupted"].IntValue);
                CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardsToCombat(curses, PileType.Draw, Owner, CardPilePosition.Random));
            }
        }
    }
}
