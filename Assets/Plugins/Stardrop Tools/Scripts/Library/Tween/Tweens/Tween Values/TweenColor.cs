
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenColor : TweenValue<Color>
    {
        public TweenColor(Color startValue, Color endValue) : base(startValue, endValue) { }

        public TweenColor(Color endValue) : base(endValue) { }

        public TweenColor() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Color;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Color.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
