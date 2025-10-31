using BepInEx.Logging;
using UnityEngine;

namespace Shapez2Mods.TimeController.UI
{
    public class TimeControlUI : MonoBehaviour
    {
        private readonly float[] speeds = { 0f, 1f, 5f, 10f, 25f };
        private readonly string[] labels = { "II", ">", ">>", ">>>", ">|" };
        private int selectedIndex = 1;
        ManualLogSource logger;

        public void Awake()
        {
             logger = BepInEx.Logging.Logger.CreateLogSource("TimeControlUI");
        }
        private void OnGUI()
        {
            if (TimeController.SimManager == null)
                return;

            GUI.BeginGroup(new Rect(Screen.width - 190, 60, 180, 40));

            for (int i = 0; i < speeds.Length; i++)
            {
                if (GUI.Button(new Rect(i * 35, 0, 35, 30), labels[i]))
                {
                    selectedIndex = i;
                    TimeController.SetSpeed(speeds[i]);
                }
            }

            GUI.EndGroup();

            if(TimeController.CurrentGameCore == null)
            {
                logger.Dispose();
                this.Destroy();
            }

        }

        private void ApplySpeed(float speed)
        {

        }
        private void Destroy()
        {
            GameObject.Destroy(this);
        }

       


    }
}
