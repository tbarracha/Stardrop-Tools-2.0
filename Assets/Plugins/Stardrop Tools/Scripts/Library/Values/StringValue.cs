﻿
namespace StardropTools
{
    public struct StringValue : IValue
    {
        private string value;

        public string String { get => value; set => SetString(value); }

        public readonly CustomEvent<string> OnValueChanged;


        public StringValue(string value)
        {
            this.value = value;
            OnValueChanged = new CustomEvent<string>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke(value);
        }

        public void SetString(string value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }
    }
}