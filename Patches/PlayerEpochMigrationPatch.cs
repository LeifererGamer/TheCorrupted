using HarmonyLib;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), nameof(ProgressSaveManager.LoadProgress))]
public static class PlayerEpochMigrationPatch
{
    [HarmonyPostfix]
    public static void Postfix(ProgressSaveManager __instance, ref ReadSaveResult<SerializableProgress> __result)
    {
        if (__result == null || !__result.Success || __result.SaveData == null)
            return;

        bool saveNeedsUpdating = false;

        Dictionary<string, string> epochMigrations = new Dictionary<string, string>()
        {
            { "CORRUPTED2_EPOCH", "THECORRUPTED-CORRUPTED2_EPOCH" },
            { "CORRUPTED3_EPOCH", "THECORRUPTED-CORRUPTED3_EPOCH" },
            { "CORRUPTED4_EPOCH", "THECORRUPTED-CORRUPTED4_EPOCH" },
            { "CORRUPTED5_EPOCH", "THECORRUPTED-CORRUPTED5_EPOCH" },
            { "CORRUPTED6_EPOCH", "THECORRUPTED-CORRUPTED6_EPOCH" },
            { "CORRUPTED7_EPOCH", "THECORRUPTED-CORRUPTED7_EPOCH" }
        };

        foreach (var savedEpoch in __result.SaveData.Epochs)
        {
            if (epochMigrations.TryGetValue(savedEpoch.Id, out string newFixedId))
            {
                if (savedEpoch.State == EpochState.Obtained || savedEpoch.State == EpochState.Revealed)
                {
                    if (!__instance.Progress.IsEpochObtained(newFixedId))
                    {
                        __instance.Progress.ObtainEpoch(newFixedId);

                        if (savedEpoch.State == EpochState.Revealed)
                        {
                            __instance.Progress.RevealEpoch(newFixedId);
                        }

                        saveNeedsUpdating = true;
                        ModEntry.Logger.Info($"[Corrupted Migration] Player updated! Migrated '{savedEpoch.Id}' to '{newFixedId}'.");
                    }
                }
            }
        }

        if (saveNeedsUpdating)
        {
            __instance.SaveProgress();
        }
    }
}