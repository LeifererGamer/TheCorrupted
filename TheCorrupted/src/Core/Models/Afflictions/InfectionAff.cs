using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Afflictions
{
    internal class InfectionAff : CustomAfflictionModel
    {
        public override async Task OnPlay(PlayerChoiceContext choiceContext, Creature? target)
        {
            foreach (Creature hittableEnemy in CombatState.HittableEnemies)
            {
                VfxCmd.PlayOnCreatureCenter(hittableEnemy, "vfx/vfx_bloody_impact");
                NWormyImpactVfx nWormyImpactVfx = NWormyImpactVfx.Create(hittableEnemy);
                if (nWormyImpactVfx != null)
                {
                    NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nWormyImpactVfx);
                }
            }
            await PowerCmd.Apply<PoisonPower>(choiceContext, CombatState.HittableEnemies, 3m, Card.Owner.Creature, Card);
        }
    }
}
