
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeQuaternionTargets<TTarget> : TweenTargets<Quaternion, TTarget>
    {
        private Vector4 intensity;

        protected TweenShakeQuaternionTargets(Quaternion endValue, Vector4 intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion endValue, Vector4 intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion endValue, Vector4 intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, Vector4 intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, Vector4 intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, Vector4 intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
        }


        // float intensity
        protected TweenShakeQuaternionTargets(Quaternion endValue, float intensity, TTarget target) : base(endValue, target)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion endValue, float intensity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion endValue, float intensity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, float intensity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected TweenShakeQuaternionTargets(Quaternion startValue, Quaternion endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeQuaternion;
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

            Lerped = Quaternion.Euler(amount) * StartValue;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
