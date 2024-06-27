using System;
using UnityEngine;

namespace StardropTools
{
    public static class MathUtils
    {
        public static float RoundDecimals(float value, int decimalAmount)
            => (float)Math.Round(value, decimalAmount);


        public static float Remap(float value, float originalRangeMin, float originalRangeMax, float targetRangeMin = 0, float targetRangeMax = 1)
        {
            // Clamp the value within the original range [a1, b1]
            float clampedValue = Mathf.Clamp(value, Mathf.Min(originalRangeMin, originalRangeMax), Mathf.Max(originalRangeMin, originalRangeMax));

            // Calculate the normalized position of the value in the original range
            float normalizedValue = (clampedValue - originalRangeMin) / (originalRangeMax - originalRangeMin);

            // Remap the normalized value to the target range [a2, b2]
            float remappedValue = normalizedValue * (targetRangeMax - targetRangeMin) + targetRangeMin;

            return remappedValue;
        }


        public static string ConvertSecondsToWorldTime(float timer)
        {
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public static int ConvertSecondsToMiliseconds(float seconds)
            => (int)(seconds * 0.001f);



        public static bool IsValueBetween(float value, float min, float max, bool inclusive = false)
        {
            if (inclusive == false)
            {
                if (value >= min && value <= max)
                    return true;
                else
                    return false;
            }

            else
            {
                if (value > min && value < max)
                    return true;
                else
                    return false;
            }
        }

        public static bool IsValueBetween(int value, int min, int max, bool inclusive = false)
        {
            if (inclusive == false)
            {
                if (value >= min && value <= max)
                    return true;
                else
                    return false;
            }

            else
            {
                if (value > min && value < max)
                    return true;
                else
                    return false;
            }
        }

        public static float PercentageBetweenMinMax(float t, float min, float max)
        {
            return Mathf.Clamp((t - min) * 1 / (max - min), 0, 1);
        }
    }
}