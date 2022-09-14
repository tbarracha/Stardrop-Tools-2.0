
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Player Pref Float")]
    public class PlayerPrefFloatSO : ScriptableObject
    {
        [SerializeField] PlayerPrefFloat prefFloat;

        public float Float => prefFloat.GetFloat();

        public void SetFloat(float value, bool save = false) => prefFloat.SetFloat(value, save);

        public float LoadFloat() => prefFloat.GetFloat(true);

        private void OnValidate()
        {
            prefFloat.Initialize();
        }
    }
}