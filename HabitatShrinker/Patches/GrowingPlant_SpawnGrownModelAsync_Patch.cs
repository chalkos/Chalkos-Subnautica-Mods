using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace HabitatShrinker.Patches
{
    /// <summary>
    /// Growing plants get adjusted according to the planter's scale by unity, but when they grow, their scale gets
    /// re-calculated in order to make it normal sized even when the planter is scaled.
    /// This just factors in the planter's scale after the game makes the plant "normal sized", to adjust it to the scale of the planter
    /// </summary>
    [HarmonyPatch(typeof(GrowingPlant), nameof(GrowingPlant.SetScale))]
    internal class GrowingPlant_SetScale_Patch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var collection = instructions.ToList();
            var codeInstructions = new List<CodeInstruction>(collection);
            var found = false;

            for (var i = 0; i < collection.Count() - 2; i++)
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

                if (currentInstruction.opcode == OpCodes.Ldloc_0
                    && secondInstruction.opcode == OpCodes.Newobj
                    && thirdInstruction.opcode == OpCodes.Callvirt)
                {
                    codeInstructions.Insert(i + 3, new CodeInstruction(OpCodes.Ldarg_0));
                    codeInstructions.Insert(i + 4, new CodeInstruction(OpCodes.Ldarg_1));
                    codeInstructions.Insert(i + 5, new CodeInstruction(OpCodes.Ldarg_2));
                    codeInstructions.Insert(i + 6,
                        new CodeInstruction(OpCodes.Call,
                            typeof(GrowingPlant_SetScale_Patch).GetMethod(nameof(AdjustGrownPlantScale))));
                    found = true;
                    break;
                }
            }

            if (found is false)
                Logger.Log(Logger.Level.Error, $"Cannot find patch location in GrowingPlant.SetScale");
            else
                Logger.Log(Logger.Level.Info, "Transpiler for GrowingPlant.SetScale completed");

            return codeInstructions.AsEnumerable();
        }

        public static void AdjustGrownPlantScale(GrowingPlant instance, Transform tr, float progress)
        {
            if (progress != 1f) // while growing, the scale is fine
                return;
            var planterScale = instance.seed.currentPlanter.transform.localScale;
            tr.localScale = Vector3.Scale(tr.localScale, planterScale);
        }
    }
}