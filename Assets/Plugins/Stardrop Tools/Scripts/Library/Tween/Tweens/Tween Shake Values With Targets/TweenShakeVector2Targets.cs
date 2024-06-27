
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeVector2Targets<TTarget> : TweenTargets<Vector2, TTarget>
    {
        private Vector2 intensity;

        protected TweenShakeVector2Targets(Vector2 endValue, Vector2 intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector2Targets(Vector2 endValue, Vector2 intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector2Targets(Vector2 endValue, Vector2 intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, Vector2 intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, Vector2 intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, Vector2 intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }


        // float intensity
        protected TweenShakeVector2Targets(Vector2 endValue, float intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected TweenShakeVector2Targets(Vector2 endValue, float intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected TweenShakeVector2Targets(Vector2 endValue, float intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, float intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected TweenShakeVector2Targets(Vector2 startValue, Vector2 endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector2;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector2 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
