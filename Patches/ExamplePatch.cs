using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Unlocks;

namespace ExampleMod.Patches;

//[HarmonyPatch(typeof(Player), nameof(Player.CreateForNewRun),
//    new Type[] { typeof(CharacterModel), typeof(UnlockState), typeof(ulong) })]
//public class ExamplePatch
//{
//    static void Postfix(Player __result)
//    {
//        // Give 999 gold at the start of the run
//        __result.Gold = 999;
//        if (__result.Character.Id.Entry.ToLowerInvariant().Equals("silent"))
//        {
//            var card = ModelDb.Card<Bodyguard>().ToMutable();
//            __result.Deck.AddInternal(card);
//        }
//    }

//}

//[HarmonyPatch(typeof(BoundPhylactery), nameof(BoundPhylactery.BeforeCombatStart))]
//internal class BoundPhylacteryStartPatch
//{
//    private static bool Prefix(BoundPhylactery __instance, ref Task __result)
//    {
//        // original base value (1)
//        var baseValue = __instance.DynamicVars.Summon.BaseValue;

//        // add the extra 5 only at combat start
//        var summonAmount = baseValue + 4m;

//        __result = OstyCmd.Summon(
//            new ThrowingPlayerChoiceContext(),
//            __instance.Owner,
//            summonAmount,
//            __instance
//        );

//        return false; // skip original method
//    }
//}