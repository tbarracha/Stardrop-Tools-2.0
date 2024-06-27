
namespace StardropTools.Tween
{
    public interface ITweenValue<TValue> : ITween
    {
        public TValue Lerped { get; }
        public TValue StartValue { get; }
        public TValue EndValue { get; }

        public ITween SetStartValue(TValue startValue);

        public ITween SetEndValue(TValue endValue);

        public ITween SetStartEndValue(TValue startValue, TValue endValue);

        public EventCallback<TValue> OnTweenValue { get; set; }

        /*
        public TValue StartValue { get; set; }
        public TValue EndValue { get; set; }
        public TValue Lerped { get; protected set; }


        public readonly CustomEvent<TValue> OnTweenValue = new CustomEvent<TValue>();


        protected TweenValue(TValue startValue, TValue endValue)
        {
            StartValue = startValue;
            EndValue = endValue;

            SetEssentials();
        }

        protected TweenValue(TValue endValue)
        {
            EndValue = endValue;
            SetEssentials();
        }

        protected TweenValue()
        {
            SetEssentials();
        }



        public ITween SetStartValue(TValue startValue)
        {
            StartValue = startValue;
            return this;
        }

        public ITween SetEndValue(TValue endValue)
        {
            EndValue = endValue;
            return this;
        }

        public ITween SetStartEndValue(TValue startValue, TValue endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            return this;
        }



        protected override void OnLoop()
        {

        }

        protected override void OnPingPong()
        {
            TValue temp = StartValue;
            StartValue = EndValue;
            EndValue = temp;
        }

        protected override void OnRemovedFromManagerList()
        {
            OnTweenValue?.ClearAllListeners();
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenValue?.Invoke(Lerped);
        }
        */
    }
}
