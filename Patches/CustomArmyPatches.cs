using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using System;

[HarmonyPatch(typeof(MonsterModel), "get_VisualsPath")]
public class CustomArmyVISUALPatche
{
    public static void Postfix(MonsterModel __instance, ref string __result)
    {
        if (__instance is Osty)
        {
            try
            {
                bool shouldUseArmyVisuals = false;

                if (TheCorrupted.TheCorrupted.src.Core.Models.Commands.ArmyCmd.IsSummoningArmy.Value)
                {
                    shouldUseArmyVisuals = true;
                }

                var owner = __instance.Creature?.PetOwner;
                if (owner != null && owner.Character.Id.Entry == "THECORRUPTED-CORRUPTED")
                {
                    shouldUseArmyVisuals = true;
                }

                if (shouldUseArmyVisuals)
                {
                    __result = "res://scenes/creature_visuals/army.tscn";
                }
            }
            catch (InvalidOperationException)
            {
                // Silently ignore during the PreloadManager phase
            }
        }
    }
}

[HarmonyPatch(typeof(MonsterModel), "get_Title")]
public class CustomArmyTitlePatch
{
    public static void Postfix(MonsterModel __instance, ref LocString __result)
    {
        if (__instance is Osty)
        {
            try
            {
                bool isMyArmy = false;

                if (TheCorrupted.TheCorrupted.src.Core.Models.Commands.ArmyCmd.IsSummoningArmy.Value)
                {
                    isMyArmy = true;
                }

                var owner = __instance.Creature?.PetOwner;
                if (owner != null && owner.Character.Id.Entry == "THECORRUPTED-CORRUPTED")
                {
                    isMyArmy = true;
                }

                if (isMyArmy)
                {
                    __result = MonsterModel.L10NMonsterLookup("ARMY.name");
                }
            }
            catch (InvalidOperationException)
            {
                // Silently ignore if this happens during early initialization
            }
        }
    }
}