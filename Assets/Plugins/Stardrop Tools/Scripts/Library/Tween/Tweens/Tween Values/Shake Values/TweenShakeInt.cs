
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before play
    /// </summary>
    public class TweenShakeInt : TweenInt
    {
        protected float intensity;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeInt;
        }

        public TweenShakeInt()
        {
            SetEssentials();
        }

        public TweenShakeInt(float intensity)
        {
            SetEssentials();
            this.intensity = intensity;
        }

        public TweenShakeInt SetIntensity(float intensity)
        {
            this.intensity = intensity;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            float amount = intensity * Ease(percent);
            lerped = Mathf.CeilToInt(start + Random.Range(-amount, amount));
        }
    }
}