using System.Collections;
using System.Linq;
using CyclopsDriftFix.GameObjects;
using HarmonyLib;
using UnityEngine;
using UWE;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix.Patches;

[HarmonyPatch(typeof(GrowingPlant))]
public class GrowingPlant_Patches
{
    [HarmonyPatch(nameof(GrowingPlant.OnEnable))]
    [HarmonyPrefix]
    static void GrowingPlant_OnEnable_Prefix(GrowingPlant __instance)
    {
        var collisions = __instance.gameObject.EnsureComponent<GrowingPlantsCollision>();
        
        __instance.growingTransform.GetComponentsInChildren<Collider>()
            .ForEach(collider => collisions.YoinkCollider(collider));
    }

    [HarmonyPatch(nameof(GrowingPlant.SetScale))]
    [HarmonyPrefix]
    static void GrowingPlant_SetScale_Prefix(GrowingPlant __instance, Transform tr, ref float progress)
    {
        progress = progress > Main.Config.VisualStartScale ? progress : Main.Config.VisualStartScale;
    }
}