
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector4 : TweenValue<Vector4>
    {
        public TweenVector4(Vector4 startValue, Vector4 endValue) : base(startValue, endValue) { }

        public TweenVector4(Vector4 endValue) : base(endValue) { }

        public TweenVector4() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector4;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector4.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
