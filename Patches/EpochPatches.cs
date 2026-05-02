using HarmonyLib;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Timeline;
using System.Linq;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Stories; // Make sure this namespace matches yours

namespace TheCorrupted.Patches;

// Inject Card Epochs (2, 5, 7)
[HarmonyPatch(typeof(SaveManager), "GetCardUnlockEpochIds")]
internal class InjectCardUnlocksPatch
{
    static void Postfix(ref string[] __result)
    {
        var list = __result.ToList();

        list.Add(EpochModel.GetId<Corrupted2Epoch>());
        list.Add(EpochModel.GetId<Corrupted5Epoch>());
        list.Add(EpochModel.GetId<Corrupted7Epoch>());

        __result = list.ToArray();
    }
}

// Inject Relic Epochs (3, 6)
[HarmonyPatch(typeof(SaveManager), "GetRelicUnlockEpochIds")]
internal class InjectRelicUnlocksPatch
{
    static void Postfix(ref string[] __result)
    {
        var list = __result.ToList();

        list.Add(EpochModel.GetId<Corrupted3Epoch>());
        list.Add(EpochModel.GetId<Corrupted6Epoch>());

        __result = list.ToArray();
    }
}

// Inject Potion Epoch (4)
[HarmonyPatch(typeof(SaveManager), "GetPotionUnlockEpochIds")]
internal class InjectPotionUnlocksPatch
{
    static void Postfix(ref string[] __result)
    {
        var list = __result.ToList();

        list.Add(EpochModel.GetId<Corrupted4Epoch>());

        __result = list.ToArray();
    }


}

[HarmonyPatch(typeof(StoryModel), "Get", new[] { typeof(string) })]
internal class InjectCustomStoryPatch
{
    // Create a singleton of your story
    private static readonly CorruptedStory _corruptedStory = new();

    static bool Prefix(string id, ref StoryModel __result)
    {
        // StringHelper.Slugify usually makes it UPPERCASE, but we check both just in case!
        if (id == "CORRUPTED" || id == "corrupted")
        {
            __result = _corruptedStory;
            return false; // Skip the base game dictionary lookup!
        }

        return true; // Let the base game handle Ironclad, Silent, etc.
    }
}