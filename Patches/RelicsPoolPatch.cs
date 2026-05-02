using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using TheCorrupted.TheCorrupted.src.Core.Models.RelicPools;
using TheCorrupted.TheCorrupted.src.Core.Models.Relics;

namespace TheCorrupted.Patches;

// 1. Add CorruptedRelicPool to the Relic Pools
[HarmonyPatch(typeof(ModelDb), "AllRelicPools", MethodType.Getter)]
[HarmonyPriority(Priority.First)]
public class RelicPoolsPatch
{
    private static void Postfix(ref IEnumerable<RelicPoolModel> __result)
    {
        var poolList = __result.ToList();

        poolList.Add(ModelDb.RelicPool<CorruptedRelicPool>());

        __result = poolList;
    }
}

// 2. Add DoomedBlade to the Event Relic Pool
[HarmonyPatch(typeof(EventRelicPool), "GenerateAllRelics")]
internal class InjectEventRelicPoolPatch
{
    static void Postfix(ref IEnumerable<RelicModel> __result)
    {
        var list = __result.ToList();

        list.Add(ModelDb.Relic<DoomedBlade>());

        __result = list;
    }
}

// 3. Add Starter/Upgrade mapping to Touch of Orobas
[HarmonyPatch(typeof(TouchOfOrobas), "RefinementUpgrades", MethodType.Getter)]
internal class TouchOfOrobasUpgradePatch
{
    static void Postfix(ref Dictionary<ModelId, RelicModel> __result)
    {
        ModelId starterId = ModelDb.Relic<CorruptedBladeRelic>().Id;

        if (!__result.ContainsKey(starterId))
        {
            __result.Add(starterId, ModelDb.Relic<DoomedBlade>());
        }
    }
}