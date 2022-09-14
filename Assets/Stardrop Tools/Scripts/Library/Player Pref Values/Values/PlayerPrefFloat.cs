
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefFloat : PlayerPrefBaseValue
    {
        [SerializeField] float startValue;
        [SerializeField] float value;

        public float Value => value;

        public override void Initialize()
        {
            prefValue = new PlayerPrefValue(startValue);
            LoadValue();
        }

        public override void LoadValue()
        {
            value = prefValue.GetFloat();
        }

        public void SetFloat(float value, bool save = false)
        {
            prefValue.SetFloat(value, save);
            this.value = value;
        }

        public float GetFloat(bool load = false)
        {
            if (load)
                LoadValue();

            return value;
        }
    }
}