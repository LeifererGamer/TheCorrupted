using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using TheCorrupted.src.Core.Models.RelicPools;

namespace TheCorrupted.Patches
{
    // Note: Verify in your decompiler if the property is exactly "AllRelicPools". 
    // It might be "RelicPools" or "AllPools" depending on the exact StS2 API version.
    [HarmonyPatch(typeof(ModelDb), "AllRelicPools", MethodType.Getter)]
    [HarmonyPriority(Priority.First)]
    public class RelicPoolsPatch
    {
        private static void Postfix(ref IEnumerable<RelicPoolModel> __result)
        {
            // Convert the existing list of pools to a usable list
            var poolList = __result.ToList();

            // Inject your custom CursedRelicPool
            poolList.Add(ModelDb.RelicPool<CorruptedRelicPool>());

            // Return the updated list to the game
            __result = poolList;
        }
    }
}