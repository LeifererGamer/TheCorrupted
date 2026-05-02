using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Screens.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCorrupted.Patches
{
    [HarmonyPatch(typeof(NMerchantCharacter), nameof(NMerchantCharacter.PlayAnimation))]
    internal class MerchantCharacterPlayAnimationPatch
    {
        // Fixes a crash, when giving up while beein at the merchant.
        public static bool Prefix(NMerchantCharacter __instance, string anim, bool loop)
        {
            if (__instance.GetChildCount() > 0)
            {
                Node firstChild = __instance.GetChild(0);

                if (firstChild.GetClass() != "SpineSprite")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
