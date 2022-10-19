using System.Collections;
using System.Linq;
using CyclopsDriftFix.GameObjects;
using HarmonyLib;
using UnityEngine;
using UWE;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix.Patches;

[HarmonyPatch(typeof(GrowingPlant), nameof(GrowingPlant.OnEnable))]
public class GrowingPlant_Enable_Patch
{
    static void Prefix(GrowingPlant __instance)
    {
        Logger.Log(Logger.Level.Fatal, "prefix run");
        __instance//.gameObject.EnsureComponent<ColliderPatcher>();
        .gameObject.GetComponentsInChildren<Collider>()
        .ForEach(collider =>
        {
            var rb = collider.gameObject.EnsureComponent<Rigidbody>();
            rb.isKinematic = true;

            collider.gameObject.EnsureComponent<CollisionIgnorer>();
        });
    }
}