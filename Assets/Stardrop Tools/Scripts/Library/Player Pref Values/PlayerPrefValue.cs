
using UnityEngine;

namespace StardropTools.PlayerPreferences
{
    [System.Serializable]
    public class PlayerPrefValue
    {
        [SerializeField] PlayerPrefValueType valueType;
        [SerializeField] string key;
#if UNITY_EDITOR
        // value is here only for us to see in inspector
        [SerializeField] string value;
#endif

        public string Key => key;

        public int IntValue => GetInt();
        public float FloatValue => GetFloat();
        public string StringValue => GetString();
        public bool BoolValue => GetBool();


        #region Constructor
        public PlayerPrefValue(PlayerPrefValueType valueType, string key, string value)
        {
            this.valueType = valueType;
            this.key = key;

            if (valueType == PlayerPrefValueType.INT)
                SetInt(int.Parse(value));
            else if (valueType == PlayerPrefValueType.FLOAT)
                SetFloat(float.Parse(value));
            else if (valueType == PlayerPrefValueType.STRING)
                SetString(value);
        }

        public PlayerPrefValue(string key, int value)
        {
            valueType = PlayerPrefValueType.INT;
            this.key = key;
            SetInt(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public PlayerPrefValue(string key, float value)
        {
            valueType = PlayerPrefValueType.FLOAT;
            this.key = key;
            SetFloat(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public PlayerPrefValue(string key, string value)
        {
            valueType = PlayerPrefValueType.STRING;
            this.key = key;
            SetString(value);

#if UNITY_EDITOR
            this.value = value;
#endif
        }

        public PlayerPrefValue(string key, bool value)
        {
            valueType = PlayerPrefValueType.BOOLEAN;
            this.key = key;
            SetBool(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        // Without key
        public PlayerPrefValue(int value)
        {
            valueType = PlayerPrefValueType.INT;
            SetInt(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public PlayerPrefValue(float value)
        {
            valueType = PlayerPrefValueType.FLOAT;
            SetFloat(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public PlayerPrefValue(string value)
        {
            valueType = PlayerPrefValueType.STRING;
            SetString(value);

#if UNITY_EDITOR
            this.value = value;
#endif
        }

        public PlayerPrefValue(bool value)
        {
            valueType = PlayerPrefValueType.BOOLEAN;
            SetBool(value);

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        #endregion // constructor


        public void SetKey(string key) => this.key = key;
        public void SetValueType(PlayerPrefValueType type) => valueType = type;


        // Setters
        public void SetInt(int value, bool save = false)
        {
            if (valueType != PlayerPrefValueType.INT)
                return;

            PlayerPrefs.SetInt(key, value);

            if (save)
                PlayerPrefs.Save();

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public void SetFloat(float value, bool save = false)
        {
            if (valueType != PlayerPrefValueType.FLOAT)
                return;

            PlayerPrefs.SetFloat(key, value);

            if (save)
                PlayerPrefs.Save();

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public void SetString(string value, bool save = false)
        {
            if (valueType != PlayerPrefValueType.STRING)
                return;

            PlayerPrefs.SetString(key, value);

            if (save)
                PlayerPrefs.Save();

#if UNITY_EDITOR
            this.value = value;
#endif
        }

        public void SetBool(bool value, bool save = false)
        {
            if (valueType != PlayerPrefValueType.BOOLEAN)
                return;

            PlayerPrefs.SetInt(key, Utilities.ConvertBoolToInt(value));

            if (save)
                PlayerPrefs.Save();

#if UNITY_EDITOR
            this.value = value.ToString();
#endif
        }

        public void SetValue(PlayerPrefValueType valueType, string value, bool save = false)
        {
            this.valueType = valueType;
#if UNITY_EDITOR
            this.value = value;
#endif

            if (save)
                PlayerPrefs.Save();
        }


        // Getters
        public int GetInt()
        {
            if (valueType != PlayerPrefValueType.INT)
                return 0;
            else
            {
#if UNITY_EDITOR
                value = PlayerPrefs.GetInt(key).ToString();
#endif

                return PlayerPrefs.GetInt(key);
            }
        }

        public float GetFloat()
        {
            if (valueType != PlayerPrefValueType.FLOAT)
                return 0;
            else
            {
#if UNITY_EDITOR
                value = PlayerPrefs.GetFloat(key).ToString();
#endif

                return PlayerPrefs.GetFloat(key);
            }
        }

        public string GetString()
        {
            if (valueType != PlayerPrefValueType.STRING)
                return null;
            else
            {
#if UNITY_EDITOR
                value = PlayerPrefs.GetString(key);
#endif

                return PlayerPrefs.GetString(key);
            }
        }

        public bool GetBool()
        {
            if (valueType != PlayerPrefValueType.BOOLEAN)
                return false;

            else
            {
#if UNITY_EDITOR
                value = Utilities.ConvertIntToBool(PlayerPrefs.GetInt(key)).ToString();
#endif

                return Utilities.ConvertIntToBool(PlayerPrefs.GetInt(key));
            }
        }


        /// <summary>
        /// This 
        /// </summary>
        public void TryGetValue()
        {
            if (key.Length == 0)
            {
                Debug.Log("Value has no key!");
                return;
            }

            if (valueType == PlayerPrefValueType.None)
            {
                Debug.Log("Value no type!");
                return;
            }


            if (valueType == PlayerPrefValueType.INT)
                GetInt();

            if (valueType == PlayerPrefValueType.FLOAT)
                GetFloat();

            if (valueType == PlayerPrefValueType.STRING)
                GetString();

            if (valueType == PlayerPrefValueType.BOOLEAN)
                GetBool();
        }
    }
}