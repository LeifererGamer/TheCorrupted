using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class BurnAff : CustomAfflictionModel
    {
        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NGroundFireVfx.Create(hittableEnemy));
                SfxCmd.Play("event:/sfx/characters/attack_fire");
            }
            await DamageCmd.Attack(2m).FromCard(Card).TargetingAllOpponents(Card.CombatState).Execute(choiceContext);
        }
    }
}