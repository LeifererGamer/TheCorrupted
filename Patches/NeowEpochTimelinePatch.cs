using HarmonyLib;
using MegaCrit.Sts2.Core.Timeline;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using System.Collections.Generic;
using System.Linq;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;

namespace TheCorrupted.Patches;

[HarmonyPatch(typeof(NeowEpoch), nameof(NeowEpoch.GetTimelineExpansion))]
internal class NeowEpochTimelinePatch
{
    static void Postfix(ref EpochModel[] __result)
    {
        // Convert the base game's array into a List so we can easily add items to it
        List<EpochModel> expandedTimeline = __result.ToList();

        // Add your custom Corrupted epochs. 
        // Because of our previous patch, EpochModel.Get() will now return your custom classes safely!

        // (Uncomment or add CORRUPTED1_EPOCH here if you have one!)
        // expandedTimeline.Add(EpochModel.Get("CORRUPTED1_EPOCH")); 

        // Example for NeowEpochTimelinePatch
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted2Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted3Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted4Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted5Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted6Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted7Epoch>()));
        // Overwrite the original result array with our new, expanded array
        __result = expandedTimeline.ToArray();
    }
}