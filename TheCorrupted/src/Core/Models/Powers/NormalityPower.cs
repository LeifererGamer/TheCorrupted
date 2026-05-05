using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class NormalityPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
        {
            modifiedCost = originalCost;
            if (card.Owner.Creature != base.Owner)
            {
                return false;
            }

            if (card.Type == CardType.Curse || card.Type == CardType.Status)
            {
                return false;
            }

            bool flag;
            switch (card.Pile?.Type)
            {
                case PileType.Hand:
                case PileType.Play:
                    flag = true;
                    break;
                default:
                    flag = false;
                    break;
            }

            if (!flag)
            {
                return false;
            }

            modifiedCost = originalCost - 1;
            return true;
        }

        public override async Task BeforeCardPlayed(CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner.Creature == base.Owner && (cardPlay.Card.Type != CardType.Curse && cardPlay.Card.Type != CardType.Status))
            {
                bool flag;
                switch (cardPlay.Card.Pile?.Type)
                {
                    case PileType.Hand:
                    case PileType.Play:
                        flag = true;
                        break;
                    default:
                        flag = false;
                        break;
                }

                if (flag)
                {
                    await PowerCmd.Decrement(this);
                }
            }
        }
    }
}