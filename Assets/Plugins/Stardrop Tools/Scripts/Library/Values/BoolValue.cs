
namespace StardropTools
{
    public struct BoolValue : IValue
    {
        private bool value;

        public bool Bool { get => value; set => SetBool(value); }

        public readonly CustomEvent<bool> OnValueChanged;


        public BoolValue(bool value)
        {
            this.value = value;

            OnValueChanged = new CustomEvent<bool>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke == false)
                return;

            OnValueChanged?.Invoke(value);
        }

        public void SetBool(bool value, bool invokeEvents = true)
        {
            this.value = value;
            InvokeEvents(invokeEvents);
        }
    }
}