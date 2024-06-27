
namespace StardropTools.Tween
{
    public abstract class TweenValue<TweenedValueType> : Tween, ITweenValue<TweenedValueType>
    {
        public TweenedValueType StartValue { get; set; }
        public TweenedValueType EndValue { get; set; }
        public TweenedValueType Lerped { get; protected set; }

        public EventCallback<TweenedValueType> OnTweenValue { get; set; } = new EventCallback<TweenedValueType>();


        protected TweenValue(TweenedValueType startValue, TweenedValueType endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            SetEssentials();
        }

        protected TweenValue(TweenedValueType endValue)
        {
            EndValue = endValue;
            SetEssentials();
        }

        protected TweenValue()
        {
            SetEssentials();
        }



        // Value
        // ------------------------------------------------------------------------------
        public ITween SetStartValue(TweenedValueType startValue)
        {
            StartValue = startValue;
            return this;
        }

        public ITween SetEndValue(TweenedValueType endValue)
        {
            EndValue = endValue;
            return this;
        }

        public ITween SetStartEndValue(TweenedValueType startValue, TweenedValueType endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            return this;
        }

        //protected override void TweenUpdate(float percent)
        //{
        //    OnTweenValue?.Invoke(Lerped);
        //}

        protected override void OnLoop()
        {

        }

        protected override void OnPingPong()
        {
            TweenedValueType temp = StartValue;
            StartValue = EndValue;
            EndValue = temp;
        }

        protected override void OnRemovedFromManagerList()
        {
            OnTweenValue?.ClearAllSubscriptions();
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
