using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct QuaternionValue : IValue<Quaternion>
    {
        private readonly Vector4Value quaternionValues;

        private readonly EventCallback<Quaternion> onValueChanged;
        public EventCallback<Quaternion> OnValueChanged => onValueChanged;

        public Quaternion Value
        {
            get => new Quaternion(quaternionValues.x, quaternionValues.y, quaternionValues.z, quaternionValues.w);
            set => SetValue(value);
        }

        public float x
        {
            get => quaternionValues.x;
            set => SetX(value);
        }

        public float y
        {
            get => quaternionValues.y;
            set => SetY(value);
        }

        public float z
        {
            get => quaternionValues.z;
            set => SetZ(value);
        }

        public float w
        {
            get => quaternionValues.w;
            set => SetW(value);
        }

        public Quaternion ToQuaternion()
        {
            return Value;
        }

        // Constructors
        public QuaternionValue(Quaternion value)
        {
            this.quaternionValues = new Vector4Value(value.x, value.y, value.z, value.w);
            this.onValueChanged = new EventCallback<Quaternion>();
        }
        public QuaternionValue(QuaternionValue value)
        {
            this.quaternionValues = new Vector4Value(value.x, value.y, value.z, value.w);
            this.onValueChanged = new EventCallback<Quaternion>();
        }

        public QuaternionValue(float x, float y, float z, float w)
        {
            this.quaternionValues = new Vector4Value(x, y, z, w);
            this.onValueChanged = new EventCallback<Quaternion>();
        }

        // Methods
        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public Quaternion GetValue()
        {
            return Value;
        }

        public void SetValue(Quaternion newValue, bool invoke = true)
        {
            quaternionValues.SetValue(newValue.x, newValue.y, newValue.z, newValue.w, false);
            InvokeEvents(invoke);
        }

        public void SetValue(QuaternionValue newValue, bool invoke = true)
        {
            quaternionValues.SetValue(newValue.x, newValue.y, newValue.z, newValue.w, false);
            InvokeEvents(invoke);
        }

        public void SetValue(float x, float y, float z, float w, bool invoke = true)
        {
            quaternionValues.SetValue(x, y, z, w, false);
            InvokeEvents(invoke);
        }

        public float GetX()
        {
            return x;
        }

        public float GetY()
        {
            return y;
        }

        public float GetZ()
        {
            return z;
        }

        public float GetW()
        {
            return w;
        }

        public void SetX(float x, bool invokeEvents = true)
        {
            quaternionValues.SetX(x, false);
            InvokeEvents(invokeEvents);
        }

        public void SetY(float y, bool invokeEvents = true)
        {
            quaternionValues.SetY(y, false);
            InvokeEvents(invokeEvents);
        }

        public void SetZ(float z, bool invokeEvents = true)
        {
            quaternionValues.SetZ(z, false);
            InvokeEvents(invokeEvents);
        }

        public void SetW(float w, bool invokeEvents = true)
        {
            quaternionValues.SetW(w, false);
            InvokeEvents(invokeEvents);
        }


        // Comparison Methods
        public bool IsEqual(QuaternionValue other)
        {
            return Value == other.Value;
        }

        public bool IsNotEqual(QuaternionValue other)
        {
            return Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is QuaternionValue quaternionValue &&
                   Value == quaternionValue.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
            //return $"{{\"x\":{Value.x},\"y\":{Value.y},\"z\":{Value.z},\"w\":{Value.w}}}";
        }

        public static QuaternionValue FromJson(string json)
        {
            QuaternionValue result = new QuaternionValue();
            try
            {
                var data = JsonUtility.FromJson<QuaternionValue>(json);
                result.SetValue(data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        public static QuaternionValue operator +(QuaternionValue a, QuaternionValue b)
        {
            return new QuaternionValue(a.Value * b.Value);
        }

        public static QuaternionValue operator -(QuaternionValue a, QuaternionValue b)
        {
            return new QuaternionValue(a.Value * Quaternion.Inverse(b.Value));
        }

        public static QuaternionValue operator *(QuaternionValue a, QuaternionValue b)
        {
            return new QuaternionValue(a.Value * b.Value);
        }

        public static QuaternionValue operator /(QuaternionValue a, QuaternionValue b)
        {
            Quaternion inverseB = Quaternion.Inverse(b.Value);
            return new QuaternionValue(a.Value * inverseB);
        }

        public static bool operator ==(QuaternionValue a, QuaternionValue b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(QuaternionValue a, QuaternionValue b)
        {
            return a.Value != b.Value;
        }
    }
}
