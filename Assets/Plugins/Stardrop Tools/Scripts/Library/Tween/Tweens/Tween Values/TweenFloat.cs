
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenFloat : TweenValue<float>
    {
        public TweenFloat(float startValue, float endValue) : base(startValue, endValue) { }

        public TweenFloat(float endValue) : base(endValue) { }

        public TweenFloat() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Float;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Mathf.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}