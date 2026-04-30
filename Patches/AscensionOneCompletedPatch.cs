using HarmonyLib;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.Timeline;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(ProgressSaveManager), "CheckAscensionOneCompleted")]
public class AscensionOneCompletedPatch
{
    static bool Prefix(ProgressSaveManager __instance, SerializablePlayer serializablePlayer, SerializableRun serializableRun)
    {
        if (serializablePlayer.CharacterId.Entry == "CORRUPTED")
        {
            if (serializableRun.Ascension == 1)
            {
                // BYPASS: Use the raw string ID!
                string correctEpochId = "CORRUPTED7_EPOCH";
                EpochModel customEpoch = EpochModel.Get(correctEpochId);

                if (customEpoch != null)
                {
                    Traverse.Create(__instance)
                        .Method("TryObtainEpochPostRun", customEpoch, serializablePlayer, serializableRun)
                        .GetValue();
                }
            }

            return false;
        }

        return true;
    }
}