
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenVector4Targets<TweenedTargetType> : TweenTargets<Vector4, TweenedTargetType>
    {
        protected TweenVector4Targets(Vector4 endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenVector4Targets(Vector4 endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenVector4Targets(Vector4 endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenVector4Targets(Vector4 startValue, Vector4 endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenVector4Targets(Vector4 startValue, Vector4 endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenVector4Targets(Vector4 startValue, Vector4 endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector4;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector4.LerpUnclamped(StartValue, EndValue, Ease(percent));
            base.TweenUpdate(percent);
        }
    }
}
