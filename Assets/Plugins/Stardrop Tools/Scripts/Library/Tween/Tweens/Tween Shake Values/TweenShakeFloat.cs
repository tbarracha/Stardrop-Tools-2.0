using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeFloat : TweenFloat
    {
        protected float intensity;

        public TweenShakeFloat(float startValue, float endValue, float intensity) : base(startValue, endValue)
        {
            this.intensity = intensity;
        }

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
