using CyclopsDriftFix.GameObjects;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace CyclopsDriftFix.Patches;

[HarmonyPatch(typeof(Player),nameof(Player.Start))]
public class TooEasyPatch
{
    [HarmonyPostfix]
    static void Postfix(GrowingPlant __instance)
    {
        // Physics.IgnoreCollision()
        //
        // __instance.GetComponentsInChildren<Collider>().ForEach(collider =>
        // {
        //     //Physics.IgnoreLayerCollision();
        //     
        //     //collider.gameObject.layer
        // });

        // __instance.GetComponentsInParent<SeaTruckSegment>().ForEach(sub =>
        // {
        //     Physics.IgnoreCollision();
        //     
        //     
        //     sub.
        // });
        
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                var result = Physics.GetIgnoreLayerCollision(i, j);
                Logger.Log(Logger.Level.Info, $"layer {i} vs {j} = {result}");
            }
        }
        
        // var collisions = __instance.gameObject.EnsureComponent<GrowingPlantsCollision>();
        //
        // __instance.growingTransform.GetComponentsInChildren<Collider>()
        //     .ForEach(collider => collisions.YoinkCollider(collider));
        //
        // collisions.Enable();
    }
}