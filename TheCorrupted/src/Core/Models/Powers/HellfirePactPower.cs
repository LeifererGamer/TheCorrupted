using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{
    internal class HellfirePactPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;


        public override PowerStackType StackType => PowerStackType.Counter;


        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
        {
            if (card.Type.Equals(CardType.Curse) || card.Type.Equals(CardType.Status) && Owner.HasPower<StatusQuoPower>())
            {
                Flash();
                await CardPileCmd.Draw(choiceContext, Amount, Owner.Player);
            }
        }
    }
}