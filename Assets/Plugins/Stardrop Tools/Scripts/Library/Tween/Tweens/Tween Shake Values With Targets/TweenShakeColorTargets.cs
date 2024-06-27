
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeColorTargets<TTarget> : TweenTargets<Color, TTarget>
    {
        private Color intensity;
        private bool shakeOpacity;

        // color intensity
        protected TweenShakeColorTargets(Color endValue, Color intensity, bool shakeOpacity, TTarget target) : base(endValue, target)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color endValue, Color intensity, bool shakeOpacity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color endValue, Color intensity, bool shakeOpacity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, Color intensity, bool shakeOpacity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, Color intensity, bool shakeOpacity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, Color intensity, bool shakeOpacity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }


        // float intensity
        protected TweenShakeColorTargets(Color endValue, float intensity, bool shakeOpacity, TTarget target) : base(endValue, target)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color endValue, float intensity, bool shakeOpacity, params TTarget[] targets) : base(endValue, targets)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color endValue, float intensity, bool shakeOpacity, List<TTarget> targets) : base(endValue, targets)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, float intensity, bool shakeOpacity, TTarget target) : base(startValue, endValue, target)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, float intensity, bool shakeOpacity, params TTarget[] targets) : base(startValue, endValue, targets)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        protected TweenShakeColorTargets(Color startValue, Color endValue, float intensity, bool shakeOpacity, List<TTarget> targets) : base(startValue, endValue, targets)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }


        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeColor;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Color shakeAmount = new Color(
                Random.Range(-intensity.r, intensity.r),
                Random.Range(-intensity.g, intensity.g),
                Random.Range(-intensity.b, intensity.b)
            );

            if (shakeOpacity)
                shakeAmount.a = Random.Range(-intensity.a, intensity.a);
            else
                shakeAmount.a = StartValue.a;

            Lerped = StartValue + shakeAmount * Ease(percent);

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
