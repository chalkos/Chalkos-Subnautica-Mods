using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using UnityEngine;

namespace CyclopsDriftFix;

[Menu("Cyclops Drift Fix")]
public class Config : ConfigFile
{
    // percentage, [0, 1] - growing plants will be fixed at this size % for interaction purposes
    // ie: you'll be able to interact with growing plants as if they were at this percentage of their growth
    public float InteractScale = 0.8f;
    
    // percentage, [0, 1] - growing plants will never be smaller (visually) than this size
    public float VisualStartScale = 0.0f;
}