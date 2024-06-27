
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenVector2Targets<TweenedTargetType> : TweenTargets<Vector2, TweenedTargetType>
    {
        protected TweenVector2Targets(Vector2 endValue, TweenedTargetType target) : base(endValue, target)
        {
        }

        protected TweenVector2Targets(Vector2 endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
        }

        protected TweenVector2Targets(Vector2 endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
        }

        protected TweenVector2Targets(Vector2 startValue, Vector2 endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
        }

        protected TweenVector2Targets(Vector2 startValue, Vector2 endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
        }

        protected TweenVector2Targets(Vector2 startValue, Vector2 endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.Vector2;
            OnTweenValueUpdate(StartValue);
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Vector2.LerpUnclamped(StartValue, EndValue, Ease(percent));
            OnTweenValueUpdate(Lerped);
            OnTweenValue?.Invoke(Lerped);
        }
    }
}
