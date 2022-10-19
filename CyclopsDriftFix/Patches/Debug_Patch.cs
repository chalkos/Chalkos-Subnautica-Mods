using System.Windows.Forms;
using CyclopsDriftFix.GameObjects;
using HarmonyLib;
using UnityEngine;

namespace CyclopsDriftFix.Patches;

[HarmonyPatch(typeof(Camera), nameof(Camera.FireOnPreRender))]
public class Debug_Camera_FireOnPreRender_Patch
{
    static void Postfix(Camera __instance)
    {
        if(Input.GetKey(KeyCode.CapsLock))
            GL.wireframe = true;
    }
}

[HarmonyPatch(typeof(Camera), nameof(Camera.FireOnPostRender))]
public class Debug_Camera_FireOnPostRender_Patch
{
    static void Postfix(Camera __instance)
    {
        GL.wireframe = false;
    }
}

//[HarmonyPatch(typeof(Collider), nameof(Collider.Instantiate))]