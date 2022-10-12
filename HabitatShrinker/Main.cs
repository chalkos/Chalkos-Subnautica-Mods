using UnityEngine;
using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using SMLHelper.V2.Handlers;
using Logger = QModManager.Utility.Logger;

namespace HabitatShrinker
{
    [QModCore]
    public static class Main
    {
        internal static Config Config { get; } = OptionsPanelHandler.Main.RegisterModOptions<Config>();
        

        internal static bool Enabled = false;
        internal static Vector3 ScaleVector => new Vector3(Config.ScaleX, Config.ScaleY, Config.ScaleZ);

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
}