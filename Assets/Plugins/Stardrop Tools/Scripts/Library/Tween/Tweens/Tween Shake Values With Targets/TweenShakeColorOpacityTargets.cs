
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenShakeColorOpacityTargets<TTarget> : TweenShakeFloatTargets<TTarget>
    {
        protected Color color;
        public Color LerpedColor => color;

        protected TweenShakeColorOpacityTargets(Color color, float startValue, float endValue, float intensity, TTarget target) : base(startValue, endValue, intensity, target)
        {
            this.color = color;
        }

        protected TweenShakeColorOpacityTargets(Color color, float startValue, float endValue, float intensity, params TTarget[] targets) : base(startValue, endValue, intensity, targets)
        {
            this.color = color;
        }

        protected TweenShakeColorOpacityTargets(Color color, float startValue, float endValue, float intensity, List<TTarget> targets) : base(startValue, endValue, intensity, targets)
        {
            this.color = color;
        }


        protected TweenShakeColorOpacityTargets(Color color, float endValue, float intensity, TTarget target) : base(endValue, intensity, target)
        {
            this.color = color;
            StartValue = color.a;
        }

        protected TweenShakeColorOpacityTargets(Color color, float endValue, float intensity, params TTarget[] targets) : base(endValue, intensity, targets)
        {
            this.color = color;
            StartValue = color.a;
        }

        protected TweenShakeColorOpacityTargets(Color color, float endValue, float intensity, List<TTarget> targets) : base(endValue, intensity, targets)
        {
            this.color = color;
            StartValue = color.a;
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeColorOpacity;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            float amount = intensity * Ease(percent);
            Lerped = StartValue + Random.Range(-amount, amount);
            color.a = Lerped;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
