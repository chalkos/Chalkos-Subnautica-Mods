using CyclopsDriftFix.GameObjects;
using HarmonyLib;
using UnityEngine;

namespace CyclopsDriftFix.Patches;

[HarmonyPatch(typeof(GrowingPlant), nameof(GrowingPlant.OnEnable))]
public class GrowingPlant_Enable_Patch
{
    static void Prefix(GrowingPlant __instance) =>
        __instance.gameObject.GetComponentsInChildren<Collider>(true)
            .ForEach(collider => collider.gameObject.EnsureComponent<CollisionIgnorer>());
}