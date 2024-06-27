
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenQuaternion : TweenValue<Quaternion>
    {
        public TweenQuaternion(Quaternion startValue, Quaternion endValue) : base(startValue, endValue) { }

        public TweenQuaternion(Quaternion endValue) : base(endValue) { }

        public TweenQuaternion() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.Quaternion;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Quaternion.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
