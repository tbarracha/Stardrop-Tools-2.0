using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.Scriptables
{
    public enum ValueType
    {
        Int,
        Float,
        Double,
        Boolean,
        String,
        Vector2,
        Vector3,
        Quaternion
    }

    [CreateAssetMenu(fileName = "Value Container", menuName = defaultMenuName + "Scriptable Value Container")]
    public class ScriptableValueContainer : ScriptableValue
    {
        [Header("Value")]
        [SerializeField] protected ValueType valueType;

        [ShowIf("valueType", ValueType.Int)]
        [SerializeField] protected int intValue;

        [ShowIf("valueType", ValueType.Float)]
        [SerializeField] protected float floatValue;

        [ShowIf("valueType", ValueType.Double)]
        [SerializeField] protected double doubleValue;

        [ShowIf("valueType", ValueType.Boolean)]
        [SerializeField] protected bool boolValue;

        [ShowIf("valueType", ValueType.String)]
        [SerializeField] protected string stringValue;

        [ShowIf("valueType", ValueType.Vector2)]
        [SerializeField] protected Vector2 vector2Value;

        [ShowIf("valueType", ValueType.Vector3)]
        [SerializeField] protected Vector3 vector3Value;

        [ShowIf("valueType", ValueType.Quaternion)]
        [SerializeField] protected Quaternion quaternionValue;

        [Header("Player Prefs")]
        [SerializeField] protected string userKey;

        protected string playerPrefKey;
        protected string TypeKey => $"{playerPrefKey}_Type";

        // Getters and setters for each value
        public int IntValue
        {
            get { return intValue; }
            set { SetIntValue(value); }
        }

        public float FloatValue
        {
            get { return floatValue; }
            set { SetFloatValue(value); }
        }

        public double DoubleValue
        {
            get { return doubleValue; }
            set { SetDoubleValue(value); }
        }

        public bool BoolValue
        {
            get { return boolValue; }
            set { SetBoolValue(value); }
        }

        public string StringValue
        {
            get { return stringValue; }
            set { SetStringValue(value); }
        }

        public Vector2 Vector2Value
        {
            get { return vector2Value; }
            set { SetVector2Value(value); }
        }

        public Vector3 Vector3Value
        {
            get { return vector3Value; }
            set { SetVector3Value(value); }
        }

        public Quaternion QuaternionValue
        {
            get { return quaternionValue; }
            set { SetQuaternionValue(value); }
        }

        public void SetIntValue(int value)
        {
            intValue = value;
        }

        public void SetFloatValue(float value)
        {
            floatValue = value;
        }

        public void SetDoubleValue(double value)
        {
            doubleValue = value;
        }

        public void SetBoolValue(bool value)
        {
            boolValue = value;
        }

        public void SetStringValue(string value)
        {
            stringValue = value;
        }

        public void SetVector2Value(Vector2 value)
        {
            vector2Value = value;
        }

        public void SetVector3Value(Vector3 value)
        {
            vector3Value = value;
        }

        public void SetQuaternionValue(Quaternion value)
        {
            quaternionValue = value;
        }

        // Save the values to PlayerPrefs
        [Button("Save")]
        public override void Save()
        {
            RefreshKey();
            Save(playerPrefKey);
        }

        public override void Save(string key)
        {
            if (IsValidKey(key, debugSave) == false)
                return;

            // save type
            PlayerPrefs.SetInt(TypeKey, (int)valueType);

            switch (valueType)
            {
                case ValueType.Int:
                    PlayerPrefs.SetInt(key, intValue);
                    break;

                case ValueType.Float:
                    PlayerPrefs.SetFloat(key, floatValue);
                    break;

                case ValueType.Double:
                    PlayerPrefs.SetString(key, doubleValue.ToString());
                    break;

                case ValueType.Boolean:
                    PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
                    break;

                case ValueType.String:
                    PlayerPrefs.SetString(key, stringValue);
                    break;

                case ValueType.Vector2:
                    PlayerPrefs.SetString(key, vector2Value.ToSerializableVector2().ToString());
                    break;

                case ValueType.Vector3:
                    PlayerPrefs.SetString(key, vector3Value.ToSerializableVector3().ToString());
                    break;

                case ValueType.Quaternion:
                    PlayerPrefs.SetString(key, quaternionValue.ToSerializableQuaternion().ToString());
                    break;
            }

            PlayerPrefs.Save();
        }

        // Load the values from PlayerPrefs
        [Button("Load")]
        public override void Load()
        {
            RefreshKey();
            Load(playerPrefKey);
        }
         
        public override void Load(string key)
        {
            if (IsValidKey(key, debugLoad) == false)
                return;

            valueType = (ValueType)PlayerPrefs.GetInt(TypeKey);

            switch (valueType)
            {
                case ValueType.Int:
                    intValue = PlayerPrefs.GetInt(key, intValue);
                    break;

                case ValueType.Float:
                    floatValue = PlayerPrefs.GetFloat(key, floatValue);
                    break;

                case ValueType.Double:
                    double.TryParse(PlayerPrefs.GetString(key, doubleValue.ToString()), out doubleValue);
                    break;

                case ValueType.Boolean:
                    boolValue = PlayerPrefs.GetInt(key, boolValue ? 1 : 0) == 1;
                    break;

                case ValueType.String:
                    stringValue = PlayerPrefs.GetString(key, stringValue);
                    break;

                case ValueType.Vector2:
                    vector2Value = SerializableVector2.TryParse(PlayerPrefs.GetString(key)).ToVector2();
                    break;

                case ValueType.Vector3:
                    vector3Value = SerializableVector3.TryParse(PlayerPrefs.GetString(key)).ToVector3();
                    break;

                case ValueType.Quaternion:
                    quaternionValue = SerializableQuaternion.TryParse(PlayerPrefs.GetString(key)).ToQuaternion();
                    break;
            }
        }

        [Button("Delete Key")]
        public override void DeleteKey()
        {
            RefreshKey();

            ValueType tempType = valueType;
            System.Array valueTypes = System.Enum.GetValues(typeof(ValueType));

            foreach (object enumValue in valueTypes)
            {
                valueType = (ValueType)enumValue;
                
                RefreshKey();
                PlayerPrefs.DeleteKey(playerPrefKey);
                PlayerPrefs.DeleteKey(TypeKey);
            }

            valueType = tempType;
            RefreshKey();
        }

        [Button("Reset")]
        public override void ResetValue()
        {
            intValue        = 0;
            floatValue      = 0;
            doubleValue     = 0;
            boolValue       = false;
            stringValue     = string.Empty;
            vector2Value    = Vector2.zero;
            vector3Value    = Vector3.zero;
            quaternionValue = Quaternion.identity;
        }

        public override void ResetValue(bool save)
        {
            ResetValue();

            if (save)
                Save();
        }

        
        private void RefreshKey()
        {
            playerPrefKey = $"{userKey}_{valueType}";
        }

        private void OnValidate()
        {
            RefreshKey();
        }
    }
}
