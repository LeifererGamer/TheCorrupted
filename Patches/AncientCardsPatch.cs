using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Ancient;
using TheCorrupted.TheCorrupted.src.Core.Models.Cards.Basic;

namespace TheCorrupted.Patches
{
    [HarmonyPatch(typeof(ArchaicTooth), "get_TranscendenceUpgrades")]
    public static class ArchaicTooth_TranscendenceUpgrades_Patch
    {
        [HarmonyPostfix]
        public static void Postfix(Dictionary<ModelId, CardModel> __result)
        {
            __result[ModelDb.Card<DefensiveRitual>().Id] = ModelDb.Card<NeowsMight>();
        }
    }
}