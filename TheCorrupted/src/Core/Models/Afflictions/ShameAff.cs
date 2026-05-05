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
    internal class ShameAff : AfflictionModel
    {
        public override bool HasExtraCardText => true;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<FrailPower>()
        ];

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            await PowerCmd.Apply<FrailPower>(Card.Owner.Creature.CombatState.HittableEnemies, 1m, Card.Owner.Creature, Card);
        }
    }
}