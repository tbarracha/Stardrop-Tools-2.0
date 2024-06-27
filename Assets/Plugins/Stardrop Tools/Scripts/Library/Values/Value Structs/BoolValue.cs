
using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct BoolValue : IValue<bool>
    {
        private bool value;

        private readonly EventCallback<bool> onValueChanged;
        public EventCallback<bool> OnValueChanged => onValueChanged;

        public bool Value { get => value; set => SetValue(value); }

        public BoolValue(bool value)
        {
            this.value = value;
            this.onValueChanged = new EventCallback<bool>();
        }

        public BoolValue(BoolValue value)
        {
            this.value = value.value;
            this.onValueChanged = new EventCallback<bool>();
        }


        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(value);
        }

        public bool GetValue()
        {
            return value;
        }

        public void SetValue(bool value, bool invoke = true)
        {
            this.value = value;
            InvokeEvents(invoke);
        }

        public void SetValue(BoolValue value, bool invoke = true)
        {
            this.value = value.value;
            InvokeEvents(invoke);
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public static BoolValue FromJson(string json)
        {
            BoolValue result = new BoolValue();
            try
            {
                var data = JsonUtility.FromJson<BoolValue>(json);
                result.SetValue(data.value);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }
    }
}