using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), "ObtainCharUnlockEpoch")]
internal class CorruptedActEpochPatch
{
    static bool Prefix(ProgressSaveManager __instance, Player localPlayer, int act)
    {
        // Only run our custom logic if the current character is the Corrupted
        if (localPlayer.Character.Id.Entry == "CORRUPTED")
        {
            EpochModel epochToUnlock = null;

            // In STS2: act 0 = Act 1 Boss, act 1 = Act 2 Boss, act 2 = Act 3 Boss
            switch (act)
            {
                case 0:
                    epochToUnlock = EpochModel.Get("CORRUPTED2_EPOCH");
                    break;
                case 1:
                    epochToUnlock = EpochModel.Get("CORRUPTED3_EPOCH");
                    break;
                case 2:
                    epochToUnlock = EpochModel.Get("CORRUPTED4_EPOCH");
                    break;
            }

            // If we successfully retrieved the epoch, tell the Save Manager to grant it
            if (epochToUnlock != null)
            {
                Traverse.Create(__instance)
                    .Method("TryObtainEpochMidRun", epochToUnlock, localPlayer)
                    .GetValue();

                ModEntry.Logger.Info($"[Corrupted Patch] Epoch obtained for completing Act {act + 1}");
            }
            else
            {
                ModEntry.Logger.Error($"[Corrupted Patch] Failed to find Epoch for Act {act + 1}!");
            }

            // Return false to stop the base game from trying to run its own string-matching logic
            return false;
        }

        // If playing Ironclad, Silent, etc., let the base game run normally
        return true;
    }
}