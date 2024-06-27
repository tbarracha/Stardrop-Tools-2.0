
using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct FloatValue : IValue<float>
    {
        private float value;

        private readonly EventCallback<float> onValueChanged;
        public EventCallback<float> OnValueChanged => onValueChanged;

        public float Value
        {
            get => value;
            set => SetValue(value);
        }

        public FloatValue(float value)
        {
            this.value = value;
            this.onValueChanged = new EventCallback<float>();
        }


        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(value);
        }

        public float GetValue()
        {
            return value;
        }

        public void SetValue(float value, bool invoke = true)
        {
            this.value = value;
            InvokeEvents(invoke);
        }



        // Mathematical Operations
        // ------------------------------------------------------------------------------

        public FloatValue Add(FloatValue other, bool invokeEvents = true)
        {
            value += other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public FloatValue Subtract(FloatValue other, bool invokeEvents = true)
        {
            value -= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public FloatValue Multiply(FloatValue other, bool invokeEvents = true)
        {
            value *= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public FloatValue Divide(FloatValue other, bool invokeEvents = true)
        {
            if (other.Value == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            value /= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Operators
        // ------------------------------------------------------------------------------

        public bool IsGreater(FloatValue other)
        {
            return value > other.value;
        }

        public bool IsLess(FloatValue other)
        {
            return value < other.value;
        }

        public bool IsGreaterOrEqual(FloatValue other)
        {
            return value >= other.value;
        }

        public bool IsLessOrEqual(FloatValue other)
        {
            return value <= other.value;
        }

        public override bool Equals(object obj)
        {
            return obj is FloatValue value &&
                   this.value == value.value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static FloatValue FromJson(string json)
        {
            FloatValue result = new FloatValue();
            try
            {
                var data = JsonUtility.FromJson<FloatValue>(json);
                result.SetValue(data.value);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        // ------------------------------------------------------------------------------

        public static FloatValue operator +(FloatValue a, FloatValue b)
        {
            return new FloatValue(a.Value + b.Value);
        }

        public static FloatValue operator -(FloatValue a, FloatValue b)
        {
            return new FloatValue(a.Value - b.Value);
        }

        public static FloatValue operator *(FloatValue a, FloatValue b)
        {
            return new FloatValue(a.Value * b.Value);
        }

        public static FloatValue operator /(FloatValue a, FloatValue b)
        {
            if (b.Value == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new FloatValue(a.Value / b.Value);
        }

        public static bool operator ==(FloatValue a, FloatValue b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(FloatValue a, FloatValue b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(FloatValue a, FloatValue b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(FloatValue a, FloatValue b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(FloatValue a, FloatValue b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(FloatValue a, FloatValue b)
        {
            return a.Value <= b.Value;
        }
    }
}