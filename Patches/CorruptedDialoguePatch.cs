using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Events;
using TheCorrupted.TheCorrupted.src.Core.Models.Characters;

internal static class CorruptedDialogueHelper
{
    public static void AddCorruptedDialogues(AncientDialogueSet dialogueSet, List<AncientDialogue> dialogues)
    {
        var watcherKey = ModelDb.Character<Corrupted>().Id.Entry;
        dialogueSet.CharacterDialogues.TryAdd(watcherKey, dialogues);
    }
}

[HarmonyPatch(typeof(TheArchitect), "DefineDialogues")]
public static class ArchitectDialoguePatch
{
    private static void Postfix(ref AncientDialogueSet __result)
    {
        CorruptedDialogueHelper.AddCorruptedDialogues(__result, 
        [
            new AncientDialogue("", "")
            {
                VisitIndex = 0, EndAttackers = ArchitectAttackers.Both
            },
            new AncientDialogue("", "", "", "")
            {
                VisitIndex = 1, EndAttackers = ArchitectAttackers.Both, IsRepeating = true
            }
        ]);
    }
}