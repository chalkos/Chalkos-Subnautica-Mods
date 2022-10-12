using HarmonyLib;
using Logger = QModManager.Utility.Logger;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace HabitatShrinker.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.Update))]
    public class Player_Update_Patch
    {
        private static bool _wasToggleKeyDown = false; 
        
        [HarmonyPostfix]
        public static void Postfix(Player __instance)
        {
            PlayerTool heldTool = Inventory.main.GetHeldTool();
            Vehicle vehicle = __instance.GetVehicle();
            Pickupable module = vehicle?.GetSlotItem(vehicle.GetActiveSlotID())?.item;

            bool builderCheck = heldTool != null && heldTool.pickupable.GetTechType() == TechType.Builder;
            bool builderModuleCheck = TechTypeHandler.TryGetModdedTechType("BuilderModule", out TechType modTechType) && module != null && module.GetTechType() == modTechType;

            // TODO: for when/if using nstrip
            //if (DevConsole.instance != null && !DevConsole.instance.state && (builderCheck || builderModuleCheck))
            if (builderCheck || builderModuleCheck)
            {
                bool isToggleKeyDown = Input.GetKeyDown(Main.Config.ToggleKey); 
                if (!_wasToggleKeyDown && isToggleKeyDown)
                {
                    Main.Enabled = !Main.Enabled;
                    
                    if(Main.Enabled)
                        Logger.Log(Logger.Level.Info, $"Size modifier enabled. X:{Main.Config.ScaleX:F2} Y:{Main.Config.ScaleY:F2} Z:{Main.Config.ScaleZ:F2}", null, true);
                    else
                        Logger.Log(Logger.Level.Info, "Size modifier disabled.", null, true);
                }

                _wasToggleKeyDown = isToggleKeyDown;
            }
            else
            {
                Main.Enabled = false;
            }
        }
    }
}