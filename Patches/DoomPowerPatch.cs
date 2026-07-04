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
    [HarmonyPatch(typeof(DoomPower), "IsOwnerDoomed")]
    public static class DoomPowerPatch
    {
        public static void Postfix(DoomPower __instance, ref bool __result)
        {
            if (__result)
            {
                if (__instance.Owner.HasPower<NeowsDoomingPower>())
                {
                    __result = false;
                }
            }
        }
    }
}