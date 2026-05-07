using HarmonyLib;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCorrupted.TheCorrupted.src.Core.Models.Afflictions;

namespace TheCorrupted.Patches
{
    [HarmonyPatch(typeof(CardModel), "HoverTips", MethodType.Getter)]
    internal class CustomAfflictionsPatch
    {
        public static void Postfix(CardModel __instance, ref IEnumerable<IHoverTip> __result)
        {
            // Check if the card currently has your custom Doubt Affliction
            if (__instance.Affliction is CustomAfflictionModel customAfflictionModel)
            {
                // Grab the exact tooltip struct that the Affliction generates
                var unwantedTip = customAfflictionModel.HoverTip;

                // Filter it out of the Card's final list.
                // We use .Equals() to safely compare the IHoverTip interface to the struct value.
                __result = __result.Where(tip => !tip.Equals(unwantedTip)).ToList();
            }
        }
    }
}
