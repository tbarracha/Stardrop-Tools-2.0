
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeFloatTargets<TTarget> : TweenTargets<float, TTarget>
    {
        protected float intensity;

        protected TweenShakeFloatTargets(float endValue, float intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeFloatTargets(float endValue, float intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeFloatTargets(float endValue, float intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeFloatTargets(float startValue, float endValue, float intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeFloatTargets(float startValue, float endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeFloatTargets(float startValue, float endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }


        // float intensity

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeFloat;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            float amount = intensity * Ease(percent);
            Lerped = StartValue + Random.Range(-amount, amount);

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
