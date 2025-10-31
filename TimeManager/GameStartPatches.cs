using Game.Core.Serialization;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapez2Mods.TimeController.UI;

namespace Shapez2Mods.TimeController.Patches
{
    [HarmonyPatch]
    public static class GameStartPatches
    {
        private static bool menuWasActive = true;

        [HarmonyPostfix]

        [HarmonyPatch(typeof(GameStartOptionsContinueExisting), MethodType.Constructor,
            new System.Type[] { typeof(SavegameBlobReader), typeof(bool), typeof(string) })]
        private static void ContinueExisting_Postfix(GameStartOptionsContinueExisting __instance)
        {
            CheckMenuMode(__instance.MenuMode);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameStartOptionsStartNew), MethodType.Constructor,
            new System.Type[] { typeof(GameParameters), typeof(bool), typeof(string) })]
        private static void StartNew_Postfix(GameStartOptionsStartNew __instance)
        {
            CheckMenuMode(__instance.MenuMode);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameCore), "Init")]
        private static void GameCore_Init_Postfix(GameCore __instance)
        {
            if (menuWasActive)
            {
                Debug.Log("[TimeController] Skipping GameCore.Init (still in menu).");
                return;
            }
            
            Debug.Log("[TimeController] GameCore.Init() detected — setting up Time Controller...");

            var sim = __instance.SimulationSpeed;
            if (sim == null)
            {
                Debug.LogWarning("[TimeController] SimulationSpeedManager not found in GameCore!");
                return;
            }
            TimeController.TryInitialize(__instance);
        }

        private static void CheckMenuMode(bool menuMode)
        {
            if (menuMode && !menuWasActive)
                menuWasActive = true;

            else if (!menuMode && menuWasActive)
            {
                // Menu closed, new or loaded game started
                menuWasActive = false;
                Debug.Log("[TimeController] Menu closed, reinitializing GameCore...");
            }
        }

        
        
    }
}
