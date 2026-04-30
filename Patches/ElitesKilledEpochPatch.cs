using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.Timeline;
using System.Collections.Generic;
using System.Linq;
using TheCorrupted.TheCorrupted.src.Core.Models.Characters;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), "CheckFifteenElitesDefeatedEpoch")]
internal class ElitesKilledEpochPatch
{
    static bool Prefix(ProgressSaveManager __instance, Player localPlayer)
    {
        if (localPlayer.Character.Id.Entry == "CORRUPTED")
        {
            // BYPASS: Use the raw string ID!
            string correctEpochId = "CORRUPTED5_EPOCH";
            EpochModel customEpoch = EpochModel.Get(correctEpochId);

            if (customEpoch == null)
            {
                Log.Error("[Corrupted Patch] Could not find Corrupted5Epoch in the game's database!");
                return false;
            }

            HashSet<ModelId> eliteEncounters = Traverse.Create(typeof(ProgressSaveManager))
                .Method("GetEliteEncounters")
                .GetValue<HashSet<ModelId>>();

            int elitesDefeated = 0;
            foreach (var encounterStat in __instance.Progress.EncounterStats.Values)
            {
                if (!eliteEncounters.Contains(encounterStat.Id)) continue;

                foreach (var fightStat in encounterStat.FightStats)
                {
                    if (fightStat.Character == localPlayer.Character.Id)
                    {
                        elitesDefeated += fightStat.Wins;
                        break;
                    }
                }
            }

            Log.Info($"[Corrupted Patch] Elites Defeated: {elitesDefeated}/15");

            if (elitesDefeated >= 15)
            {
                Traverse.Create(__instance)
                    .Method("TryObtainEpochMidRun", customEpoch, localPlayer)
                    .GetValue();

                Log.Info($"[Corrupted Patch] Successfully granted Elite Epoch: {correctEpochId}");
            }

            return false;
        }

        return true;
    }
}