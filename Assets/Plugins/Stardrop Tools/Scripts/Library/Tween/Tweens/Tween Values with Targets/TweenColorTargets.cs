
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenColorTargets<TweenedTargetType> : TweenTargets<Color, TweenedTargetType>
    {
        protected TweenColorTargets(Color endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenColorTargets(Color endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenColorTargets(Color endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenColorTargets(Color startValue, Color endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenColorTargets(Color startValue, Color endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenColorTargets(Color startValue, Color endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Color;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Color.LerpUnclamped(StartValue, EndValue, Ease(percent));
            base.TweenUpdate(percent);
        }
    }
}
