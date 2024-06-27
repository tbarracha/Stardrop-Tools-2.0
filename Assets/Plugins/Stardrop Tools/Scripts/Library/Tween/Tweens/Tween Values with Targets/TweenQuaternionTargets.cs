
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenQuaternionTargets<TweenedTargetType> : TweenTargets<Quaternion, TweenedTargetType>
    {
        protected TweenQuaternionTargets(Quaternion endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenQuaternionTargets(Quaternion endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenQuaternionTargets(Quaternion endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenQuaternionTargets(Quaternion startValue, Quaternion endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenQuaternionTargets(Quaternion startValue, Quaternion endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenQuaternionTargets(Quaternion startValue, Quaternion endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Quaternion;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Quaternion.SlerpUnclamped(StartValue, EndValue, Ease(percent));
            base.TweenUpdate(percent);
        }
    }
}
