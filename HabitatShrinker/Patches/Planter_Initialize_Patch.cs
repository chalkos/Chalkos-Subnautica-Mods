using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace HabitatShrinker.Patches
{
    /// <summary>
    /// Used to make sure the scale of grown plants will be the same as the scale of the planter.
    /// This does run more times than needed, but it's inexpensive and makes sure the grown plant will fetch the correct scale  
    /// </summary>
    [HarmonyPatch(typeof(Planter), "Initialize")]
    public class Planter_Initialize_Patch
    {
        [HarmonyPostfix]
        public static void Planter_Initialize_Postfix(ref Planter __instance)
        {
            // __instance.grownPlantsRoot.localScale = new Vector3(1, 1, 1);
            //     //__instance.transform.localScale;
            // Logger.Log(Logger.Level.Info, $"planter {__instance.transform.localScale}->{__instance.grownPlantsRoot.localScale}", null, true);
            //
            // for (int i = 0; i < __instance.grownPlantsRoot.childCount; i++)
            // {
            //     var t = __instance.grownPlantsRoot.GetChild(i);
            //     t.localScale = new Vector3(1, 1, 1);
            // }
        }
    }
}