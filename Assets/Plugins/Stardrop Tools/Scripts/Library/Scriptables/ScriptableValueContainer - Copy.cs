using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.Scriptables
{
    [CreateAssetMenu(fileName = "Defaultable Value Container", menuName = defaultMenuName + "Scriptable Defaultable Value Container")]
    public class ScriptableDefaultableValueContainer : ScriptableValueContainer
    {
        [Header("Default Values")]
        [ShowIf("valueType", ValueType.Int)]
        [SerializeField] private int defaultIntValue;

        [ShowIf("valueType", ValueType.Float)]
        [SerializeField] private float defaultFloatValue;

        [ShowIf("valueType", ValueType.Double)]
        [SerializeField] private double defaultDoubleValue;

        [ShowIf("valueType", ValueType.Boolean)]
        [SerializeField] private bool defaultBoolValue;

        [ShowIf("valueType", ValueType.String)]
        [SerializeField] private string defaultStringValue;

        [ShowIf("valueType", ValueType.Vector2)]
        [SerializeField] private Vector2 defaultVector2Value;

        [ShowIf("valueType", ValueType.Vector3)]
        [SerializeField] private Vector3 defaultVector3Value;

        [ShowIf("valueType", ValueType.Quaternion)]
        [SerializeField] private Quaternion defaultQuaternionValue;

        public int DefaultIntValue { get => defaultIntValue; set => defaultIntValue = value; }
        public float DefaultFloatValue { get => defaultFloatValue; set => defaultFloatValue = value; }
        public double DefaultDoubleValue { get => defaultDoubleValue; set => defaultDoubleValue = value; }
        public bool DefaultBoolValue { get => defaultBoolValue; set => defaultBoolValue = value; }
        public string DefaultStringValue { get => defaultStringValue; set => defaultStringValue = value; }
        public Vector2 DefaultVector2Value { get => defaultVector2Value; set => defaultVector2Value = value; }
        public Vector3 DefaultVector3Value { get => defaultVector3Value; set => defaultVector3Value = value; }
        public Quaternion DefaultQuaternionValue { get => defaultQuaternionValue; set => defaultQuaternionValue = value; }

        public void ResetToDefault()
        {
            intValue        = defaultIntValue;
            floatValue      = defaultFloatValue;
            doubleValue     = defaultDoubleValue;
            boolValue       = defaultBoolValue;
            stringValue     = defaultStringValue;
            vector2Value    = defaultVector2Value;
            vector3Value    = defaultVector3Value;
            quaternionValue = defaultQuaternionValue;
        }
    }
}
