
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Player Pref String")]
    public class PlayerPrefStringSO : ScriptableObject
    {
        [SerializeField] PlayerPrefString prefString;

        public string String => prefString.GetString();
        public void SetInd(string value, bool save = false) => prefString.SetString(value, save);

        public string LoadInt() => prefString.GetString(true);

        private void OnValidate()
        {
            prefString.Initialize();
        }
    }
}