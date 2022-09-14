
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public abstract class PlayerPrefBaseValue
    {
        [SerializeField] protected PlayerPrefValue prefValue;

        public abstract void Initialize();
        public abstract void LoadValue();
    }
}