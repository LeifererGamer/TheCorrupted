using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Powers;

namespace TheCorrupted.Patches
{
    [HarmonyPatch(typeof(DoomPower), nameof(DoomPower.GetDoomedCreatures))]
    public static class DoomPowerPatch
    {
        public static void Postfix(ref IReadOnlyList<Creature> __result)
        {
            // Take the original result and filter out anyone with NeowsDoomingCorruptionPower
            __result = __result
                .Where(c => !c.HasPower<NeowsDoomingPower>())
                .ToList();
        }
    }
}