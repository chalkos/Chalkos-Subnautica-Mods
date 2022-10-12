using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace HabitatShrinker.Patches
{
    /// <summary>
    /// grown plants' scale gets adjusted when they grow, in order to show them at normal size independently of the parent scale
    /// so they need to be scaled down to 1 and just follow the parent
    /// </summary>
    [HarmonyPatch(typeof(GrowingPlant), "SpawnGrownModelAsync")]
    public class GrowingPlant_SpawnGrownModelAsync_Patch
    {
        static void Postfix(ref GrowingPlant __instance, ref bool ___isIndoor,
            ref AnimationCurve ___growthWidthIndoor,
            ref AnimationCurve ___growthHeightIndoor,
            ref AnimationCurve ___growthWidth,
            ref AnimationCurve ___growthHeight)
        {
            // Logger.Log(Logger.Level.Info, $"spawn transform", null, true);
            //
            // float progress = 1f;
            // float num = ___isIndoor ? ___growthWidthIndoor.Evaluate(progress) : ___growthWidth.Evaluate(progress);
            // float y = ___isIndoor ? ___growthHeightIndoor.Evaluate(progress) : ___growthHeight.Evaluate(progress);
            //
            // for (int i = 0; i < __instance.seed.currentPlanter.grownPlantsRoot.childCount; i++)
            // {
            //     __instance.seed.currentPlanter.grownPlantsRoot.GetChild(i).localScale = new Vector3(1, 1, 1);
            // }
        }
    }

    [HarmonyPatch(typeof(GrowingPlant), nameof(GrowingPlant.SetScale))]
    internal class GrowingPlant_SetScale_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var collection = instructions.ToList();
            var codeInstructions = new List<CodeInstruction>(collection);
            var found = false;

            for(var i = 0; i < collection.Count() - 2; i++)
            {
                var currentInstruction = codeInstructions[i];
                var secondInstruction = codeInstructions[i + 1];
                var thirdInstruction = codeInstructions[i + 2];

                //          ldarg.0      // this
                //
                // [126 5 - 126 45]
                // IL_0046: ldarg.1      // tr
                // IL_0047: ldloc.0      // num
                // IL_0048: ldloc.1      // y
                // IL_0049: ldloc.0      // num
                // IL_004a: newobj       instance void [UnityEngine.CoreModule]UnityEngine.Vector3::.ctor(float32, float32, float32)
                // IL_004f: callvirt     instance void [UnityEngine.CoreModule]UnityEngine.Transform::set_localScale(valuetype [UnityEngine.CoreModule]UnityEngine.Vector3)

                if(currentInstruction.opcode == OpCodes.Ldloc_0
                   && secondInstruction.opcode == OpCodes.Newobj
                   && thirdInstruction.opcode == OpCodes.Callvirt)
                {
                    codeInstructions.Insert(i + 3, new CodeInstruction(OpCodes.Ldarg_0));
                    codeInstructions.Insert(i + 4, new CodeInstruction(OpCodes.Ldarg_1));
                    codeInstructions.Insert(i + 5, new CodeInstruction(OpCodes.Ldarg_2));
                    codeInstructions.Insert(i + 6, new CodeInstruction(OpCodes.Call, typeof(GrowingPlant_SetScale_Patch).GetMethod(nameof(FixedScale))));
                    found = true;
                    break;
                }
            }

            if(found is false)
                Logger.Log(Logger.Level.Error, $"Cannot find patch location in GrowingPlant.SetScale");
            else
                Logger.Log(Logger.Level.Info, "Transpiler for GrowingPlant.SetScale completed");

            return codeInstructions.AsEnumerable();
        }

        public static void FixedScale(GrowingPlant instance, Transform tr, float progress)
        {
            if (progress != 1f)
                return;
            var planterScale = instance.seed.currentPlanter.transform.localScale;
            tr.localScale = Vector3.Scale(tr.localScale, planterScale);
        }
    }

    
}