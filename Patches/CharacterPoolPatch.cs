using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

using System.Reflection;
using TheCorrupted.TheCorrupted.src.Core.Models.Characters;

namespace TheCorrupted.Patches
{
    [HarmonyPatch(typeof(ModelDb), "AllCharacters", MethodType.Getter)]
    [HarmonyPriority(Priority.First)]
    public class ModelDbAllCharactersPatch
    {
        private static void Postfix(ref IEnumerable<CharacterModel> __result)
        {
            // Add Watcher to the list of all characters
            var charactersList = __result.ToList();
            charactersList.Add(ModelDb.Character<Corrupted>());


            __result = charactersList;


            typeof(ModelDb).GetField("_allCharacterCardPools", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, null);
            typeof(ModelDb).GetField("_allCards", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, null);
        }
    }
}


