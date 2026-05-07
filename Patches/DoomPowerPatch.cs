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
    [HarmonyPatch(typeof(DoomPower), nameof(DoomPower.IsOwnerDoomed))]
    public static class DoomPowerPatch
    {
        public static void Postfix(DoomPower __instance, ref bool __result)
        {
            // If the original game said they were doomed...
            if (__result)
            {
                // ...but they have our custom power...
                if (__instance.Owner.HasPower<NeowsDoomingPower>())
                {
                    // ...then they are not actually doomed!
                    __result = false;
                }
            }
        }
    }
}