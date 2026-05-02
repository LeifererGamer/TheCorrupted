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
        List<EpochModel> expandedTimeline = __result.ToList();

        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted2Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted3Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted4Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted5Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted6Epoch>()));
        expandedTimeline.Add(EpochModel.Get(EpochModel.GetId<Corrupted7Epoch>()));

        __result = expandedTimeline.ToArray();
    }
}