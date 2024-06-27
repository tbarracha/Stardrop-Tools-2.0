
namespace StardropTools.Values
{
    public interface IValue<T>
    {
        public T Value { get; set; }
        public EventCallback<T> OnValueChanged { get; }

        void InvokeEvents(bool invoke);

        T GetValue();
        void SetValue(T value, bool invoke = true);
    }
}