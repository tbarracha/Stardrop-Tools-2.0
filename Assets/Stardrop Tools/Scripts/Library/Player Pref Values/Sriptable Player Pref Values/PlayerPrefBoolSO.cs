
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Player Pref Bool")]
    public class PlayerPrefBoolSO : ScriptableObject
    {
        [SerializeField] PlayerPrefBool prefBool;

        public bool Int => prefBool.GetBool();

        public void SetInd(bool value, bool save = false) => prefBool.SetBool(value, save);

        public bool LoadInt() => prefBool.GetBool(true);

        private void OnValidate()
        {
            prefBool.Initialize();
        }
    }
}