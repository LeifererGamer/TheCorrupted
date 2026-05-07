using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
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

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class RegretAff : CustomAfflictionModel
    {
        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(hittableEnemy, VfxColor.Purple));
                await CreatureCmd.Damage(choiceContext, hittableEnemy, Card.Owner.Piles.Where(p => p.Type == PileType.Hand).First().Cards.Count(), ValueProp.Unblockable | ValueProp.Unpowered, Card.Owner.Creature, null);
            }
            
        }
    }
}