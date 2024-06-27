using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct IntValue : IValue<int>
    {
        [SerializeField]
        private int value;

        private readonly EventCallback<int> onValueChanged;
        public EventCallback<int> OnValueChanged => onValueChanged;

        public int Value
        {
            get => value;
            set => SetValue(value);
        }

        public IntValue(int value)
        {
            this.value = value;
            this.onValueChanged = new EventCallback<int>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(value);
        }

        public int GetValue()
        {
            return value;
        }

        public void SetValue(int value, bool invoke = true)
        {
            this.value = value;
            InvokeEvents(invoke);
        }



        // Mathematical Operations
        // ------------------------------------------------------------------------------
        // IntValue
        public IntValue Add(IntValue other, bool invokeEvents = true)
        {
            value += other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Subtract(IntValue other, bool invokeEvents = true)
        {
            value -= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Multiply(IntValue other, bool invokeEvents = true)
        {
            value *= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Divide(IntValue other, bool invokeEvents = true)
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


        // Primitive int
        public IntValue Add(int other, bool invokeEvents = true)
        {
            value += other;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Subtract(int other, bool invokeEvents = true)
        {
            value -= other;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Multiply(int other, bool invokeEvents = true)
        {
            value *= other;
            InvokeEvents(invokeEvents);
            return this;
        }

        public IntValue Divide(int other, bool invokeEvents = true)
        {
            if (other == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            value /= other;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Operators
        // ------------------------------------------------------------------------------

        // IntValue
        public bool IsGreater(IntValue other)
        {
            return value > other.value;
        }

        public bool IsLess(IntValue other)
        {
            return value < other.value;
        }

        public bool IsGreaterOrEqual(IntValue other)
        {
            return value >= other.value;
        }

        public bool IsLessOrEqual(IntValue other)
        {
            return value <= other.value;
        }


        // Primitive int
        public bool IsGreater(int other)
        {
            return value > other;
        }

        public bool IsLess(int other)
        {
            return value < other;
        }

        public bool IsGreaterOrEqual(int other)
        {
            return value >= other;
        }

        public bool IsLessOrEqual(int other)
        {
            return value <= other;
        }

        public override bool Equals(object obj)
        {
            return obj is IntValue intValue &&
                   value == intValue.value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static IntValue FromJson(string json)
        {
            IntValue result = new IntValue();
            try
            {
                var data = JsonUtility.FromJson<IntValue>(json);
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

        // IntValue
        public static IntValue operator +(IntValue a, IntValue b)
        {
            IntValue intValue = new IntValue(a.Value + b.Value);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator -(IntValue a, IntValue b)
        {
            IntValue intValue = new IntValue(a.Value - b.Value);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator *(IntValue a, IntValue b)
        {
            IntValue intValue = new IntValue(a.Value * b.Value);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator /(IntValue a, IntValue b)
        {
            if (b.Value == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new IntValue(a.Value / b.Value);
        }

        public static bool operator ==(IntValue a, IntValue b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(IntValue a, IntValue b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(IntValue a, IntValue b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(IntValue a, IntValue b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(IntValue a, IntValue b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(IntValue a, IntValue b)
        {
            return a.Value <= b.Value;
        }

        // Primitive int
        public static IntValue operator +(IntValue a, int b)
        {
            IntValue intValue = new IntValue(a.Value + b);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator -(IntValue a, int b)
        {
            IntValue intValue = new IntValue(a.Value - b);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator *(IntValue a, int b)
        {
            IntValue intValue = new IntValue(a.Value * b);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static IntValue operator /(IntValue a, int b)
        {
            if (b == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            IntValue intValue = new IntValue(a.Value / b);
            intValue.InvokeEvents(true);
            return intValue;
        }

        public static bool operator ==(IntValue a, int b)
        {
            return a.Value == b;
        }

        public static bool operator !=(IntValue a, int b)
        {
            return a.Value != b;
        }

        public static bool operator >(IntValue a, int b)
        {
            return a.Value > b;
        }

        public static bool operator <(IntValue a, int b)
        {
            return a.Value < b;
        }

        public static bool operator >=(IntValue a, int b)
        {
            return a.Value >= b;
        }

        public static bool operator <=(IntValue a, int b)
        {
            return a.Value <= b;
        }
    }
}