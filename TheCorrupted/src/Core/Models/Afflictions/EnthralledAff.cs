using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class EnthralledAff : CustomAfflictionModel
    {
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.ForEnergy(Card)
        ];

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            await PlayerCmd.GainEnergy(2m, Card.Owner);
        }
    }
}