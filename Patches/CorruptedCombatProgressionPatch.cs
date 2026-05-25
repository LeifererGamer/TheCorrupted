using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;
using System.Collections.Generic;
using System.Linq;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), nameof(ProgressSaveManager.UpdateAfterCombatWon))]
public static class CorruptedCombatProgressionPatch
{
    [HarmonyPostfix]
    public static void Postfix(ProgressSaveManager __instance, Player localPlayer, CombatRoom room)
    {
        if (localPlayer.Character.Id.Entry.ToUpperInvariant() != "THECORRUPTED-CORRUPTED")
            return;

        bool savedChanges = false;

        // ==========================================
        // BOSS ENCOUNTERS (Act Unlocks & 15 Bosses)
        // ==========================================
        if (room.RoomType == RoomType.Boss)
        {
            // --- ACT UNLOCKS ---
            int currentAct = room.CombatState.RunState.CurrentActIndex;
            string actEpochId = currentAct switch
            {
                0 => "THECORRUPTED-CORRUPTED2_EPOCH",
                1 => "THECORRUPTED-CORRUPTED3_EPOCH",
                2 => "THECORRUPTED-CORRUPTED4_EPOCH",
                _ => null
            };

            if (actEpochId != null)
            {
                savedChanges |= TryUnlockEpoch(__instance, actEpochId, localPlayer);
                ModEntry.Logger.Info($"[Corrupted] Act {currentAct + 1} Boss defeated! Checking Act unlock...");
            }
            int totalBossKills = CalculateTotalKills(__instance, localPlayer.Character.Id, isBoss: true);
            ModEntry.Logger.Info($"[Corrupted] Total Boss Kills: {totalBossKills}/15");

            if (totalBossKills >= 15)
            {
                savedChanges |= TryUnlockEpoch(__instance, "THECORRUPTED-CORRUPTED6_EPOCH", localPlayer);
            }
        }

        // ==========================================
        // ELITE ENCOUNTERS (15 Elites Unlock)
        // ==========================================
        else if (room.RoomType == RoomType.Elite)
        {
            int totalEliteKills = CalculateTotalKills(__instance, localPlayer.Character.Id, isBoss: false);
            ModEntry.Logger.Info($"[Corrupted] Total Elite Kills: {totalEliteKills}/15");

            if (totalEliteKills >= 15)
            {
                savedChanges |= TryUnlockEpoch(__instance, "THECORRUPTED-CORRUPTED5_EPOCH", localPlayer);
            }
        }

        if (savedChanges)
        {
            SaveManager.Instance.SaveProgressFile();
        }
    }

    private static bool TryUnlockEpoch(ProgressSaveManager manager, string epochId, Player player)
    {
        if (!SaveManager.Instance.Progress.IsEpochObtained(epochId) &&
            !player.DiscoveredEpochs.Contains(epochId))
        {
            EpochModel model = EpochModel.Get(epochId);
            if (model != null)
            {
                Traverse.Create(manager).Method("TryObtainEpochMidRun", model, player).GetValue();
                ModEntry.Logger.Info($"[Corrupted] Successfully unlocked Epoch: {epochId}");
                return true;
            }
        }
        return false;
    }

    private static int CalculateTotalKills(ProgressSaveManager manager, ModelId characterId, bool isBoss)
    {
        HashSet<ModelId> validEncounterIds;

        if (isBoss)
        {
            validEncounterIds = ModelDb.Acts
                .SelectMany(act => act.AllBossEncounters.Select(enc => enc.Id))
                .ToHashSet();
        }
        else
        {
            validEncounterIds = Traverse.Create(typeof(ProgressSaveManager))
                .Method("GetEliteEncounters")
                .GetValue<HashSet<ModelId>>();
        }

        int totalKills = 0;
        foreach (var encounterStat in manager.Progress.EncounterStats.Values)
        {
            if (!validEncounterIds.Contains(encounterStat.Id)) continue;

            var stats = encounterStat.FightStats.FirstOrDefault(f => f.Character == characterId);
            if (stats != null)
            {
                totalKills += stats.Wins;
            }
        }
        return totalKills;
    }
}