using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

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
            }

            switch (category)
            {
                case TechCategory.InteriorPiece:
                case TechCategory.BasePiece:
                    return Main.Config.SafetyOverride;
                default:
                    return true;
            }
        }

        private static TechType GetLastTechType =>
#if SN1
            Builder.constructableTechType;
#elif BZ
            Builder.lastTechType;
#endif

        public static void Prefix()
        {
            if (Builder.prefab == null || !ShouldScaleTechType(GetLastTechType)) return;

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