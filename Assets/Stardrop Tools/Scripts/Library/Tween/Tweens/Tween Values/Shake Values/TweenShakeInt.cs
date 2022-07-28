
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeInt : TweenInt
    {
        protected int intensity;

        public TweenShakeInt()
        {
            tweenType = TweenType.ShakeInt;
        }

        public TweenShakeInt SetIntensity(int intensity)
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
            lerped = (int)(start + Random.Range(-amount, amount));
        }
    }
}