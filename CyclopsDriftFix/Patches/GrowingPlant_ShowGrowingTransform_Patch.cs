using System.Collections;
using System.Linq;
using CyclopsDriftFix.GameObjects;
using HarmonyLib;
//using HarmonyLib;
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
        
        
        // var collisions = __instance.gameObject.EnsureComponent<GrowingPlantsCollision>();
        //
        // __instance.growingTransform.GetComponentsInChildren<Collider>()
        //     .ForEach(collider => collisions.YoinkCollider(collider));
        //
        // collisions.Enable();
    }

    //[HarmonyPatch(nameof(GrowingPlant.OnDisable))]
    //[HarmonyPostfix]
    static void GrowingPlant_OnDisable_Postfix(GrowingPlant __instance)
    {
        if (__instance.gameObject.GetComponent<GrowingPlantsCollision>() is { } collisions)
            collisions.Disable();
    }

    //[HarmonyPatch(nameof(GrowingPlant.SetScale))]
    //[HarmonyPrefix]
    static void GrowingPlant_SetScale_Prefix(GrowingPlant __instance, Transform tr, ref float progress)
    {
        if (progress >= 1f && __instance.gameObject.GetComponent<GrowingPlantsCollision>() is { } collisions)
            collisions.Disable();

        progress = progress > Main.Config.VisualStartScale ? progress : Main.Config.VisualStartScale;
    }
}