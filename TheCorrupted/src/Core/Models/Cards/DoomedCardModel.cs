using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Cards
{
    internal abstract class DoomedCardModel(int cost, CardType type, CardRarity rarity, TargetType target)
        : TheCorruptedCardModel(cost, type, rarity, target)
    {
        public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (!participants.ToList().Contains(Owner.Creature)) return;

            if (Pile.Type.Equals(PileType.Hand))
            {
                IEnumerable<Creature> creatures = [Owner.Creature];
                await PowerCmd.Apply<DoomPower>(choiceContext, creatures, DynamicVars["Doomed"].BaseValue, Owner.Creature, this);
                await CardCmd.AutoPlay(choiceContext, this, null);
            }
        }
    }

}