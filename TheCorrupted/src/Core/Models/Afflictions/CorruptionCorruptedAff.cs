using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class CorruptionCorruptedAff : AfflictionModel
    {
        public override bool HasExtraCardText => true;

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {

        }
    }
}