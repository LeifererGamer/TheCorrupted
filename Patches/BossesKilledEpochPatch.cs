using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;
using System.Collections.Generic;
using System.Linq;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), "CheckFifteenBossesDefeatedEpoch")]
[HarmonyPatch(typeof(ProgressSaveManager), "CheckFifteenBossesDefeatedEpoch")]
public class BossesKilledEpochPatch
{
    static bool Prefix(ProgressSaveManager __instance, Player localPlayer)
    {
        if (localPlayer.Character.Id.Entry == "CORRUPTED")
        {
            int bossesDefeated = CalculateBossKills(__instance, localPlayer);

            // Act 1 Boss Kill -> Unlock Epoch 2
            if (bossesDefeated >= 1)
            {
                Unlock(__instance, "CORRUPTED2_EPOCH", localPlayer);
            }
            // 15 Bosses -> Unlock Epoch 6
            if (bossesDefeated >= 15)
            {
                Unlock(__instance, "CORRUPTED6_EPOCH", localPlayer);
            }
            return false;
        }
        return true;
    }

    private static void Unlock(ProgressSaveManager manager, string id, Player player)
    {
        EpochModel model = EpochModel.Get(id);
        if (model == null) return;

        // Standard vanilla unlock
        Traverse.Create(manager).Method("TryObtainEpochMidRun", model, player).GetValue();

        // Manual save force
        var serializable = manager.Progress.Epochs.FirstOrDefault(e => e.Id == id);
        if (serializable != null) serializable.State = EpochState.Obtained;
    }

    private static int CalculateBossKills(ProgressSaveManager instance, Player player)
    {
        // ... (Keep your existing boss counting logic here)
        return 0; // Placeholder
    }
}