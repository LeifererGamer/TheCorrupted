using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using System.Reflection;

[ModInitializer("Initialize")]
public class ModEntry
{
    public static void Initialize()
    {
        var harmony = new Harmony("cursed.patch");
        Log.Info("Cursed");

        var assembly = Assembly.GetExecutingAssembly();
        ScriptManagerBridge.LookupScriptsInAssembly(assembly);

        //ProgressSaveManagerCustomCharPatch.Apply(harmony);
        harmony.PatchAll();
    }
}

