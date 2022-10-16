using System.Numerics;
using UnityEngine;
using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix;

[QModCore]
public static class Main
{
    internal static Config Config { get; } = OptionsPanelHandler.Main.RegisterModOptions<Config>();

    [QModPatch]
    public static void Patch()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var modName = $"chalkos_{assembly.GetName().Name}";
        Logger.Log(Logger.Level.Info, $"Patching {modName}");
        Harmony harmony = new Harmony(modName);
        harmony.PatchAll(assembly);
        Logger.Log(Logger.Level.Info, "Patched successfully!");
    }
}