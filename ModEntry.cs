using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using System.Reflection;
using Logger = MegaCrit.Sts2.Core.Logging.Logger;

namespace TheCorrupted;

[ModInitializer(nameof(Initialize))]
public partial class ModEntry : Node // Make this partial and inherit from Node
{
    public const string ModId = "TheCorrupted";

    public static Logger Logger { get; } = new(ModId, LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        var assembly = Assembly.GetExecutingAssembly();

        // This is the magic line. It will NOW find your character because it is "partial"!
        ScriptManagerBridge.LookupScriptsInAssembly(assembly);

        harmony.PatchAll();
    }
}