
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector2 : TweenValue<Vector2>
    {
        public TweenVector2(Vector2 startValue, Vector2 endValue) : base(startValue, endValue) { }

        public TweenVector2(Vector2 endValue) : base(endValue) { }

        public TweenVector2() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector2;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector2.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
