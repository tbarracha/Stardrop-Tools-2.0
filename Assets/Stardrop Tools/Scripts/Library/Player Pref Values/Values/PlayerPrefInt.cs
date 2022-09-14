
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefInt : PlayerPrefBaseValue
    {
        [SerializeField] int startValue;
        [SerializeField] int value;

        public int Value => value;

        public override void Initialize()
        {
            prefValue = new PlayerPrefValue(startValue);
            LoadValue();
        }

        public override void LoadValue()
        {
            value = prefValue.GetInt();
        }

        public void SetInt(int value, bool save = false)
        {
            prefValue.SetInt(value, save);
            this.value = value;
        }

        public int GetInt(bool load = false)
        {
            if (load)
                LoadValue();

            return value;
        }
    }
}