
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenVector3Targets<TweenedTargetType> : TweenTargets<Vector3, TweenedTargetType>
    {
        protected TweenVector3Targets(Vector3 endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenVector3Targets(Vector3 endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenVector3Targets(Vector3 endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenVector3Targets(Vector3 startValue, Vector3 endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenVector3Targets(Vector3 startValue, Vector3 endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenVector3Targets(Vector3 startValue, Vector3 endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector3;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector3.LerpUnclamped(StartValue, EndValue, Ease(percent));
            base.TweenUpdate(percent);
        }
    }
}
