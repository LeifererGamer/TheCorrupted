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
public class BossesKilledEpochPatch
{
    static bool Prefix(ProgressSaveManager __instance, Player localPlayer)
    {
        // Intercept logic only for the custom character
        if (localPlayer.Character.Id.Entry == "CORRUPTED")
        {
            int bossesDefeated = CalculateBossKills(__instance, localPlayer);
           //ModEntry.Logger.Info($"[Corrupted Patch] Bosses Defeated: {bossesDefeated}/15");

            if (bossesDefeated >= 15)
            {
                Unlock(__instance, "CORRUPTED6_EPOCH", localPlayer);
            }

            // Skip the original method
            return false;
        }

        // Run the original method for base game characters
        return true;
    }

    private static void Unlock(ProgressSaveManager manager, string id, Player player)
    {
        EpochModel model = EpochModel.Get(id);
        if (model == null) return;

        // Use Traverse to invoke the private TryObtainEpochMidRun method
        Traverse.Create(manager).Method("TryObtainEpochMidRun", model, player).GetValue();

        // Ensure the state is saved in the serializable progress data
        var serializable = manager.Progress.Epochs.FirstOrDefault(e => e.Id == id);
        if (serializable != null)
        {
            serializable.State = EpochState.Obtained;
        }
    }

    private static int CalculateBossKills(ProgressSaveManager manager, Player player)
    {
        // Get a HashSet of all valid Boss Encounter IDs from the game's database
        HashSet<ModelId> bossEncounterIds = ModelDb.Acts
            .SelectMany((ActModel act) => act.AllBossEncounters.Select((EncounterModel enc) => enc.Id))
            .ToHashSet();

        int totalBossKills = 0;

        // Iterate through the saved encounter statistics
        foreach (EncounterStats encounterStat in manager.Progress.EncounterStats.Values)
        {
            // Skip if the encounter wasn't a boss
            if (!bossEncounterIds.Contains(encounterStat.Id))
            {
                continue;
            }

            // Find the fight stats specifically for our custom character
            foreach (FightStats fightStat in encounterStat.FightStats)
            {
                if (fightStat.Character == player.Character.Id)
                {
                    totalBossKills += fightStat.Wins;
                    break;
                }
            }
        }

        return totalBossKills;
    }
}