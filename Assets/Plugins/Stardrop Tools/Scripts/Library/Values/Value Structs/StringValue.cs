using System;
using System.Text;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct StringValue : IValue<string>
    {
        private readonly StringBuilder value;

        private readonly EventCallback<string> onValueChanged;
        public EventCallback<string> OnValueChanged => onValueChanged;

        public string Value
        {
            get => value.ToString();
            set => SetValue(value);
        }

        public StringValue(string value)
        {
            this.value = new StringBuilder(value);
            this.onValueChanged = new EventCallback<string>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public string GetValue()
        {
            return Value;
        }

        public void SetValue(string newValue, bool invoke = true)
        {
            if (newValue == null)
                newValue = string.Empty;

            value.Clear();
            value.Append(newValue);
            InvokeEvents(invoke);
        }



        // Concatenation
        // ------------------------------------------------------------------------------

        public StringValue Concatenate(StringValue other, bool invokeEvents = true)
        {
            value.Append(other.Value);
            InvokeEvents(invokeEvents);
            return this;
        }

        public StringValue Concatenate(string text, bool invokeEvents = true)
        {
            value.Append(text);
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Operators
        // ------------------------------------------------------------------------------

        public bool IsEqual(StringValue other)
        {
            return value.ToString() == other.Value;
        }

        public bool IsNotEqual(StringValue other)
        {
            return value.ToString() != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is StringValue stringValue &&
                   value.ToString() == stringValue.value.ToString();
        }

        public override int GetHashCode()
        {
            return value.ToString().GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static StringValue FromJson(string json)
        {
            StringValue result = new StringValue();
            try
            {
                var data = JsonUtility.FromJson<StringValue>(json);
                result.SetValue(data.ToString());
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        // ------------------------------------------------------------------------------

        public static StringValue operator +(StringValue a, StringValue b)
        {
            return new StringValue(a.Value + b.Value);
        }

        public static bool operator ==(StringValue a, StringValue b)
        {
            return a.value.ToString() == b.Value;
        }

        public static bool operator !=(StringValue a, StringValue b)
        {
            return a.value.ToString() != b.Value;
        }
    }
}
