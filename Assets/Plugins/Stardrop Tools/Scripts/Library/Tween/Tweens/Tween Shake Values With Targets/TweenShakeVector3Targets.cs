
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeVector3Targets<TTarget> : TweenTargets<Vector3, TTarget>
    {
        protected Vector3 intensity;

        protected TweenShakeVector3Targets(Vector3 endValue, Vector3 intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector3Targets(Vector3 endValue, Vector3 intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector3Targets(Vector3 endValue, Vector3 intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, Vector3 intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, Vector3 intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, Vector3 intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }


        // float intensity
        protected TweenShakeVector3Targets(Vector3 endValue, float intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected TweenShakeVector3Targets(Vector3 endValue, float intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected TweenShakeVector3Targets(Vector3 endValue, float intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, float intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected TweenShakeVector3Targets(Vector3 startValue, Vector3 endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector3;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector3 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
