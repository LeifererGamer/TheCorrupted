using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class NormalityAff : AfflictionModel
    {
        public override bool HasExtraCardText => true;

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            await PowerCmd.Apply<NormalityPower>([Card.Owner.Creature], 3m, Card.Owner.Creature, Card);
        }
    }
}