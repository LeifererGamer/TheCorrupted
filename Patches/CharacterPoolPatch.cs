using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;

using System.Reflection;
using TheCorrupted.TheCorrupted.src.Core.Models.Characters;

namespace TheCorrupted.Patches
{
    namespace TheCorrupted.Patches
    {
        [HarmonyPatch(typeof(ModelDb), "AllCharacters", MethodType.Getter)]
        [HarmonyPriority(Priority.First)]
        public class ModelDbAllCharactersPatch
        {
            private static void Postfix(ref IEnumerable<CharacterModel> __result)
            {
                // Safely append the Corrupted character
                var charactersList = __result.ToList();
                charactersList.Add(ModelDb.Character<Corrupted>());

                __result = charactersList;
            }
        }
    }
}


