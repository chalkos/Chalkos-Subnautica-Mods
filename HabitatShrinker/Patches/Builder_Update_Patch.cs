using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace HabitatShrinker.Patches
{
    [HarmonyPatch(typeof(Builder), nameof(Builder.Update))]
    internal class Builder_Update_Patch
    {
        private static readonly Dictionary<TechType, TechCategory> _techTypeCategoryCache = new Dictionary<TechType, TechCategory>();

        private static bool ShouldScaleTechType(TechType techType)
        {
            if (!_techTypeCategoryCache.TryGetValue(techType, out var category))
            {
                if (!CraftData.GetBuilderIndex(techType, out _, out category, out _))
                    category = TechCategory.Misc;
                _techTypeCategoryCache[techType] = category;
            }

            switch(category)
            {
                case TechCategory.InteriorPiece:
                case TechCategory.BasePiece:
                    return Main.Config.SafetyOverride;
                default:
                    return true;
            }
        }

        [HarmonyPrefix]
        public static void Builder_Update_Prefix(ref Vector3 ___ghostModelScale, ref GameObject ___prefab)
        {
            if (___prefab == null || !ShouldScaleTechType(Builder.lastTechType)) return;

            var scale = Main.Enabled ? Main.ScaleVector : Vector3.one;
            ___ghostModelScale = scale;
            ___prefab.transform.localScale = scale;
        }
    }
}