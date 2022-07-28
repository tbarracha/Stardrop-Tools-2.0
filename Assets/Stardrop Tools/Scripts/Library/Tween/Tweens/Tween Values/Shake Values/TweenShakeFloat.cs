
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeFloat : TweenFloat
    {
        protected float intensity;

        public TweenShakeFloat()
        {
            tweenType = TweenType.ShakeFloat;
        }

        public TweenShakeFloat SetIntensity(float intensity)
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
            lerped = start + Random.Range(-amount, amount);
        }
    }
}