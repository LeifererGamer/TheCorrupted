using HarmonyLib;
using MegaCrit.Sts2.Core.Timeline;
using System;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs; // Adjust to your actual namespace

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(EpochModel), "Get", new Type[] { typeof(string) })]
internal class RegisterCustomEpochsPatch
{
    static bool Prefix(string id, ref EpochModel __result)
    {
        // Intercept requests for your custom epoch IDs
        switch (id)
        {
            case "CORRUPTED2_EPOCH":
                __result = new Corrupted2Epoch();
                return false; // Skip the base game's Get method so it doesn't throw an error

            case "CORRUPTED3_EPOCH":
                __result = new Corrupted3Epoch();
                return false;

            case "CORRUPTED4_EPOCH":
                __result = new Corrupted4Epoch();
                return false;

            case "CORRUPTED5_EPOCH":
                __result = new Corrupted5Epoch(); // This fixes your current crash!
                return false;

            case "CORRUPTED6_EPOCH":
                __result = new Corrupted6Epoch();
                return false;

            case "CORRUPTED7_EPOCH":
                __result = new Corrupted7Epoch();
                return false;
        }

        // If it's a base game Epoch (or another mod's), let the original method run normally
        return true;
    }
}