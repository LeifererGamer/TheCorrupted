using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCorrupted.src.Core.Models.Commands;

namespace TheCorrupted.src.Core.Models.Cards.Common
{
    internal abstract class DoomedCardModel(int cost, CardType type, CardRarity rarity, TargetType target)
        : TheCorruptedCardModel(cost, type, rarity, target)
    {
        public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
        {
            if (side != base.Owner.Creature.Side)
            {
                return;
            }
            if (this.Pile.Type.Equals(PileType.Hand))
            {
                IEnumerable<Creature> creatures = [base.Owner.Creature];
                await CardCmd.AutoPlay(choiceContext, this, null);
                await PowerCmd.Apply<DoomPower>(creatures, base.DynamicVars["Doomed"].BaseValue, base.Owner.Creature, this);
            }
        }
    }

}