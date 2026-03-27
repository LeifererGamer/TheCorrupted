using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.src.Core.Models.Powers
{

internal class SavedByCorruptionPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new HashSet<IHoverTip> { HoverTipFactory.Static(StaticHoverTip.Block) };

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
        {
            if (card.Type.Equals(CardType.Curse) || card.Type.Equals(CardType.Status) && base.Owner.HasPower<StatusQuoPower>())
            {
                Flash();
                await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null);
            }
        }
        public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
        {
            if (card.Type.Equals(CardType.Curse) || card.Type.Equals(CardType.Status) && base.Owner.HasPower<StatusQuoPower>())
            {
                Flash();
                await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null);
            }
        }
    }
}