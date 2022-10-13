using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace HabitatShrinker
{
    [Menu("Habitat Shrinker")]
    public class Config : ConfigFile
    {
        private const float Min = 0.05f;
        private const float Max = 2.5f;
        private const float Step = 0.05f;
        private const float Default = 1.0f;
        private const string Tooltip = "0.5 is half size, 1 is default size, 2 is double size";

        [Keybind("Toggle on/off")] public KeyCode ToggleKey = KeyCode.I;

        [Slider("Scale X", Format = "{0:F2}", Min = Min, Max = Max, DefaultValue = Default, Step = Step, Tooltip = Tooltip)]
        public float ScaleX = Default;

        [Slider("Scale Y", Format = "{0:F2}", Min = Min, Max = Max, DefaultValue = Default, Step = Step, Tooltip = Tooltip)]
        public float ScaleY = Default;

        [Slider("Scale Z", Format = "{0:F2}", Min = Min, Max = Max, DefaultValue = Default, Step = Step, Tooltip = Tooltip)]
        public float ScaleZ = Default;

        [Toggle("Safety override", Tooltip = "Modify size of base pieces and vehicles. Expect bugs with this on.")]
        public bool SafetyOverride = false;
    }
}