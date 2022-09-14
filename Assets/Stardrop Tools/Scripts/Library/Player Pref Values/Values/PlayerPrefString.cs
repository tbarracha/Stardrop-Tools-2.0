
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefString : PlayerPrefBaseValue
    {
        [SerializeField] string startValue;
        [SerializeField] string value;

        public string Value => value;

        public override void Initialize()
        {
            prefValue = new PlayerPrefValue(startValue);
            LoadValue();
        }

        public override void LoadValue()
        {
            value = prefValue.GetString();
        }

        public void SetString(string value, bool save = false)
        {
            prefValue.SetString(value, save);
            this.value = value;
        }

        public string GetString(bool load = false)
        {
            if (load)
                LoadValue();

            return value;
        }
    }
}