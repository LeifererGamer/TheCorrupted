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
        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != Owner.Creature.Side)
            {
                return;
            }
            if (Pile.Type.Equals(PileType.Hand))
            {
                IEnumerable<Creature> creatures = [Owner.Creature];
                await CardCmd.AutoPlay(choiceContext, this, null);
                await PowerCmd.Apply<DoomPower>(creatures, DynamicVars["Doomed"].BaseValue, Owner.Creature, this);
            }
        }
    }

}