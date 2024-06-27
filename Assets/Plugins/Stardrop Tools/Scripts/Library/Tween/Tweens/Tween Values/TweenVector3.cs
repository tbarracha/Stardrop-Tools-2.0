
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector3 : TweenValue<Vector3>
    {
        public TweenVector3(Vector3 startValue, Vector3 endValue) : base(startValue, endValue) { }

        public TweenVector3(Vector3 endValue) : base(endValue) { }

        public TweenVector3() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector3;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector3.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
