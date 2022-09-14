
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Preferences / Player Pref Int")]
    public class PlayerPrefIntSO : ScriptableObject
    {
        [SerializeField] PlayerPrefInt prefInt;

        public int Int => prefInt.GetInt();

        public void SetInd(int value, bool save = false) => prefInt.SetInt(value, save);

        public int LoadInt() => prefInt.GetInt(true);

        private void OnValidate()
        {
            prefInt.Initialize();
        }
    }
}