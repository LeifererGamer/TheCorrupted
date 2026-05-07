using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class BeckonAff : CustomAfflictionModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
        HoverTipFactory.FromPower<DoomPower>(),
        ];

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            await PowerCmd.Apply<DoomPower>([Card.Owner.Creature], 6, Card.Owner.Creature, null);
        }
    }
}