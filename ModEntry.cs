using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Timeline;
using System.Reflection;
using TheCorrupted.TheCorrupted.src.Core.Timeline.Epochs;
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
        ScriptManagerBridge.LookupScriptsInAssembly(assembly);

        InjectCustomEpochs();

        harmony.PatchAll();
    }

    private static void InjectCustomEpochs()
    {
        // 1. Grab the private, locked dictionaries inside the base game
        var typeDict = Traverse.Create(typeof(EpochModel)).Field("_epochTypeDictionary").GetValue<Dictionary<string, Type>>();
        var idDict = Traverse.Create(typeof(EpochModel)).Field("_typeToIdDictionary").GetValue<Dictionary<Type, string>>();

        // 2. Force the base game to build its hardcoded list of 57 IDs, then grab it so we can expand it
        _ = EpochModel.AllEpochIds;
        var allIdsList = Traverse.Create(typeof(EpochModel)).Field("_allEpochIds").GetValue<List<string>>();

        // 3. Define custom Epochs
        Type[] myEpochs = new Type[]
        {
            typeof(Corrupted2Epoch),
            typeof(Corrupted3Epoch),
            typeof(Corrupted4Epoch),
            typeof(Corrupted5Epoch),
            typeof(Corrupted6Epoch),
            typeof(Corrupted7Epoch)
        };

        // 4. Inject them into the game's brain
        foreach (Type t in myEpochs)
        {
            EpochModel instance = (EpochModel)Activator.CreateInstance(t);

            typeDict[instance.Id] = t;
            idDict[t] = instance.Id;

            if (!allIdsList.Contains(instance.Id))
            {
                allIdsList.Add(instance.Id);
            }
        }

        Logger.Info("[Corrupted Patch] Successfully bypassed hardcoded limits and injected Custom Epochs!");
    }
}