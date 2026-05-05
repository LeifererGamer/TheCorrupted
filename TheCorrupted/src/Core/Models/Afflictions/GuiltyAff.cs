using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class GuiltyAff : AfflictionModel
    {
        public override bool HasExtraCardText => true;

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {

        }
    }
}