using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Logger = QModManager.Utility.Logger;

namespace HabitatShrinker.Patches
{
    /// <summary>
    /// Updates the scale when building, changing the ghost scale, the "being built" scale, and the final fully-constructed scale
    /// </summary>
    [HarmonyPatch(typeof(Builder), nameof(Builder.Update))]
    internal class Builder_Update_Patch
    {
        private static readonly Dictionary<TechType, TechCategory> _techTypeCategoryCache =
            new Dictionary<TechType, TechCategory>();

        private static bool ShouldScaleTechType(TechType techType)
        {
            if (!_techTypeCategoryCache.TryGetValue(techType, out var category))
            {
                if (!CraftData.GetBuilderIndex(techType, out _, out category, out _))
                    category = TechCategory.Misc;
                _techTypeCategoryCache[techType] = category;
                //Logger.Log(Logger.Level.Info, $"Found techtype: {techType}", null, true);
            }

            switch (category)
            {
                case TechCategory.InteriorPiece:
                case TechCategory.BasePiece:
#if SN1
                case TechCategory.BaseRoom:
                case TechCategory.BaseWall:
#endif
                    return Main.Config.SafetyOverride;
                default:
                    return true;
            }
        }

        public static void Prefix()
        {
            if (Builder.prefab == null ||
                !ShouldScaleTechType(Builder.prefab.GetComponent<Constructable>().techType)) return;

            var scale = Main.Enabled ? Main.ScaleVector : Vector3.one;

            if (Builder.prefab.transform.localScale != scale)
            {
                Builder.prefab.transform.localScale = scale;
                Object.Destroy((Object)Builder.ghostModel);
                Builder.Update();
            }
        }
    }
}