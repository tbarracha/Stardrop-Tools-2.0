
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefBool : PlayerPrefBaseValue
    {
        [SerializeField] bool startValue;
        [SerializeField] bool value;

        public bool Value => value;

        public override void Initialize()
        {
            prefValue = new PlayerPrefValue(startValue);
            LoadValue();
        }

        public override void LoadValue()
        {
            value = prefValue.GetBool();
        }

        public void SetBool(bool value, bool save = false)
        {
            prefValue.SetBool(value, save);
            this.value = value;
        }

        public bool GetBool(bool load = false)
        {
            if (load)
                LoadValue();

            return value;
        }
    }
}