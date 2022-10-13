using HarmonyLib;
using SMLHelper.V2.Handlers;
using UnityEngine;

namespace HabitatShrinker.Patches
{
    /// <summary>
    /// Used to monitor keypress to toggle the scaling functionality on/off.
    /// It can only be turned on with the builder tool in hand, and will turn off afterwards (like Building tweaks).
    /// 
    /// Compatibility:
    /// - BuilderModule: the scaling functionality can also be toggled when using a Builder Module
    /// </summary>
    [HarmonyPatch(typeof(Player), nameof(Player.Update))]
    public class Player_Update_Patch
    {
        private static bool _wasToggleKeyDown = false;

        private static string _currentMessage = null;

        [HarmonyPostfix]
        public static void Postfix(Player __instance)
        {
            PlayerTool heldTool = Inventory.main.GetHeldTool();
            Vehicle vehicle = __instance.GetVehicle();
            Pickupable module = vehicle?.GetSlotItem(vehicle.GetActiveSlotID())?.item;

            bool builderCheck = heldTool != null && heldTool.pickupable.GetTechType() == TechType.Builder;
            bool builderModuleCheck = TechTypeHandler.TryGetModdedTechType("BuilderModule", out TechType modTechType) &&
                                      module != null && module.GetTechType() == modTechType;

            if (DevConsole.instance != null && !DevConsole.instance.state && (builderCheck || builderModuleCheck))
            {
                bool isToggleKeyDown = Input.GetKeyDown(Main.Config.ToggleKey);
                if (!_wasToggleKeyDown && isToggleKeyDown)
                {
                    ProcessMessage(_currentMessage, false);
                    Main.Enabled = !Main.Enabled;
                    _currentMessage = Main.Enabled
                        ? $"Size modifier enabled. X:{Main.Config.ScaleX:F2} Y:{Main.Config.ScaleY:F2} Z:{Main.Config.ScaleZ:F2}"
                        : "Size modifier disabled";
                    ProcessMessage(_currentMessage, true);
                }

                _wasToggleKeyDown = isToggleKeyDown;
            }
            else if(Main.Enabled)
            {
                ProcessMessage(_currentMessage, false);
                Main.Enabled = false;
            }
        }

        private static void ProcessMessage(string msg, bool active)
        {
            if (msg == null) return;
            var message = ErrorMessage.main.GetExistingMessage(msg);
            if (active)
            {
                if (message != null)
                {
                    message.messageText = msg;
                    message.entry.text = msg;
                    if (message.timeEnd <= Time.time + 1f)
                        message.timeEnd += Time.deltaTime;
                }
                else
                {
                    ErrorMessage.AddMessage(msg);
                }
            }
            else if (message != null && message.timeEnd > Time.time)
            {
                message.timeEnd = Time.time;
            }
        }
    }
}