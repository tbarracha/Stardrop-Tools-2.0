using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public static class TimeManager
    {
        // Dictionary to hold time scales for different processes
        private static Dictionary<string, float> timeScales = new Dictionary<string, float>();

        // Method to set time scale for a specific process
        public static void SetTimeScale(string processName, float scale)
        {
            if (timeScales.ContainsKey(processName))
            {
                timeScales[processName] = scale;
            }
            else
            {
                timeScales.Add(processName, scale);
            }
        }

        // Method to get time scale for a specific process
        public static float GetTimeScale(string processName)
        {
            if (timeScales.ContainsKey(processName))
            {
                return timeScales[processName];
            }
            else
            {
                return 1f; // Default time scale
            }
        }

        // Get custom delta time for a specific process
        public static float GetDeltaTime(string processName)
        {
            return Time.deltaTime * GetTimeScale(processName);
        }
    }
}
