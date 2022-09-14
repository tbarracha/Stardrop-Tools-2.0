using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.PlayerPreferences
{
    [CreateAssetMenu(menuName = "Stardrop / Player Pref Values")]
    public class PlayerPrefValuesSO : ScriptableObject
    {
#if UNITY_EDITOR
        [ResizableTextArea][SerializeField] string description = "(EX: 0-sound effects, 1-music)";
        [SerializeField] bool deleteDuplicates;
#endif
        [SerializeField] List<PlayerPrefValue> values;

        public void TryGetValues()
        {
            for (int i = 0; i < values.Count; i++)
                values[i].TryGetValue();
        }

        public PlayerPrefValue GetValue(int valueIndex) => values[valueIndex];
        public PlayerPrefValue GetValue(string key)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Key.Length == key.Length && values[i].Key == key)
                    return values[i];
            }

            Debug.Log("No value found with key: " + key);
            return null;
        }

        // Setters
        public void SetInt(int valueIndex, int value, bool save = false) => GetValue(valueIndex).SetInt(value, save);
        public void SetInt(string key, int value, bool save = false) => GetValue(key).SetInt(value, save);

        public void SetFloat(int valueIndex, float value, bool save = false) => GetValue(valueIndex).SetFloat(value, save);
        public void SetFloat(string key, float value, bool save = false) => GetValue(key).SetFloat(value, save);

        public void SetString(int valueIndex, string value, bool save = false) => GetValue(valueIndex).SetString(value, save);
        public void SetString(string key, string value, bool save = false) => GetValue(key).SetString(value, save);

        public void SetBool(int valueIndex, bool value, bool save = false) => GetValue(valueIndex).SetBool(value, save);
        public void SetBool(string key, bool value, bool save = false) => GetValue(key).SetBool(value, save);


        // Getters
        public int GetInt(int valueIndex) => GetValue(valueIndex).GetInt();
        public int GetInt(string key) => GetValue(key).GetInt();
        
        public float GetFloat(int valueIndex) => GetValue(valueIndex).GetFloat();
        public float GetFloat(string key) => GetValue(key).GetFloat();
        
        public string GetString(int valueIndex) => GetValue(valueIndex).GetString();
        public string GetString(string key) => GetValue(key).GetString();
        
        public bool GetBool(int valueIndex) => GetValue(valueIndex).GetBool();
        public bool GetBool(string key) => GetValue(key).GetBool();



        public void AddValue(PlayerPrefValue value)
        {
            if (values.Contains(value) == false)
                values.Add(value);
        }

        public void RemoveValue(PlayerPrefValue value)
        {
            if (values.Contains(value) == false)
                values.Remove(value);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < values.Count; i++)
            {
                var value = values[i];

                for (int j = 0; j < values.Count; j++)
                {
                    // ignore concurrent j-i value (when they are the same)
                    if (j == i)
                        continue;

                    var val = values[j];

                    if (value.Key.Length == val.Key.Length && value.Key == val.Key)
                    {
                        Debug.Log("Value at index: " + j + ", is duplicate of value at index: " + i);

                        if (deleteDuplicates)
                            values.RemoveAt(j);
                    }
                }
            }
        }
#endif
    }
}