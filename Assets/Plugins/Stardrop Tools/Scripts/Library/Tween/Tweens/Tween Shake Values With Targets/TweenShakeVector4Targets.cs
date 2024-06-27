
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeVector4Targets<TTarget> : TweenTargets<Vector4, TTarget>
    {
        private Vector4 intensity;

        protected TweenShakeVector4Targets(Vector4 endValue, Vector4 intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector4Targets(Vector4 endValue, Vector4 intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector4Targets(Vector4 endValue, Vector4 intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, Vector4 intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, Vector4 intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, Vector4 intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }


        // float intensity
        protected TweenShakeVector4Targets(Vector4 endValue, float intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeVector4Targets(Vector4 endValue, float intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeVector4Targets(Vector4 endValue, float intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, float intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeVector4Targets(Vector4 startValue, Vector4 endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }


        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector4;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector4 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);
            amount.w = Random.Range(-amount.w, amount.w);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
