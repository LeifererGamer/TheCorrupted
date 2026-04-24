using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.TheCorrupted.src.Core.Models.Powers
{

internal class DoomsdayPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<DoomPower>()
        ];

        public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (!(amount <= 0m) && applier == base.Owner && power is DoomPower)
            {
                Flash();
                foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
                {
                    NFireBurstVfx child = NFireBurstVfx.Create(hittableEnemy, 0.75f);
                    NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(child);
                }

                // 1. Get the current Network ID (required for multiplayer syncing)
                ulong? netId = LocalContext.NetId;

                // 2. Fallback check: If netId is null, we can't create the context safely.
                if (netId.HasValue)
                {
                    // 3. Create the automated context using this Power (this) as the source model
                    HookPlayerChoiceContext choiceContext = new HookPlayerChoiceContext(
                        this,                 // The AbstractModel triggering the action (your Power)
                        netId.Value,          // The network ID of the host/player
                        base.CombatState,     // The current combat state
                        GameActionType.Combat // The type of action
                    );

                    // 4. Pass the newly created context into the Damage command
                    await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
                }
                else
                {
                    // Optional: Log an error here if needed. 
                    // In vanilla STS2, if netId is null, the game usually just skips the action to prevent a crash.
                }
            }
        }
    }
}