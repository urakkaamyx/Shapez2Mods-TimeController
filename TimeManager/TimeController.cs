using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Shapez2Mods.TimeController.UI;
using Shapez2Mods.TimeController.Patches;

namespace Shapez2Mods.TimeController
{
    [BepInPlugin("urakka.shapez2mods.timecontroller", "Time Controller", "1.0.0")]
    public class TimeController : BaseUnityPlugin
    {
        
        internal static TimeController Instance;
        internal static GameCore CurrentGameCore;
        internal static SimulationSpeedManager SimManager;
        internal static TimeControlUI UI;

        private void Awake()
        {
            Instance = this;
            var harmony = new Harmony("urakka.shapez2mods.timecontroller");
            harmony.PatchAll();
            Logger.LogInfo("Time Controller loaded.");
        }

        public static void TryInitialize(GameCore GC)
        {
            Instance.Logger.LogInfo("[TimeController] TryInitialize");
            CurrentGameCore = GC;


            if (CurrentGameCore == null)
            {
                Debug.LogWarning("[TimeController] GameCore not found yet.");
                return;
            }

            SimManager = CurrentGameCore.SimulationSpeed;
            if (SimManager == null)
            {
                Debug.LogWarning("[TimeController] SimulationSpeedManager not found.");
                return;
            }

            Debug.Log("[TimeController] SimulationSpeedManager found.");
            CreateUI();
            if (UI == null)
            {
                
                ApplyInitialSpeed();
            }

        }

        private static void ApplyInitialSpeed()
        {
            if (SimManager == null) return;

            // Example: set starting speed to normal (1x)
            SetSpeed(1.0f);
        }

        public static void SetSpeed(float speed)
        {
            Instance.Logger.LogInfo("Speed trying to be set");

            if (SimManager == null)
            {
                Instance.Logger.LogInfo("Simulation manager not found!");
                return;
            }

            if (speed == 0f)
            {
                SimManager.IsPaused = true;
            }
            SimManager.Speed = speed;
            if (SimManager.Speed != speed)
            {
                Instance.Logger.LogInfo($" Failed to set speed to {speed}x");
            }
            else
            {
                Instance.Logger.LogInfo($"[TimeController] Speed set to {speed}x");
            }
        }

        private static void CreateUI()
        {
            if (GameObject.Find("BepInEx_Manager") != null)
            {
                var uiObj = GameObject.Find("BepInEx_Manager").AddComponent<TimeControlUI>();

                Instance.Logger.LogInfo("[TimeController] TimeControlUI created.");
            }

        }

        
    }
}
