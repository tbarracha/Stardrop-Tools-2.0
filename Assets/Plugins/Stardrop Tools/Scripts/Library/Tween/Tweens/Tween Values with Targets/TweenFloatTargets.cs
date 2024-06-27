
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenFloatTargets<TweenedTargetType> : TweenTargets<float, TweenedTargetType>
    {
        protected TweenFloatTargets(float endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenFloatTargets(float endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenFloatTargets(float endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenFloatTargets(float startValue, float endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenFloatTargets(float startValue, float endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenFloatTargets(float startValue, float endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Float;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Mathf.LerpUnclamped(StartValue, EndValue, Ease(percent));
            base.TweenUpdate(percent);
        }
    }
}
