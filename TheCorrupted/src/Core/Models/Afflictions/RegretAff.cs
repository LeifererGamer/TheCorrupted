using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class RegretAff : AfflictionModel
    {
        public override bool HasExtraCardText => true;

        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NFireBurstVfx child = NFireBurstVfx.Create(hittableEnemy, 0.75f);
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);

            }
            await DamageCmd.Attack(Card.Owner.Piles.Where(p => p.Type == PileType.Hand).Count()).FromCard(Card).TargetingAllOpponents(Card.CombatState).Execute(choiceContext);
        }
    }
}