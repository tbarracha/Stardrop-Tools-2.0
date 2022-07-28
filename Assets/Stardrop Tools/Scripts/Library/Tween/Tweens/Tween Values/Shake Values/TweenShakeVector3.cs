
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeVector3 : TweenVector3
    {
        protected Vector3 intensity;

        public TweenShakeVector3()
        {
            tweenType = TweenType.ShakeVector3;
        }

        public TweenShakeVector3 SetIntensity(Vector3 intensity)
        {
            this.intensity = intensity;
            return this;
        }

        public TweenShakeVector3 SetIntensity(float intensityMultiplier)
        {
            intensity = Vector3.one * intensityMultiplier;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            Vector3 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);

            lerped = amount + start;
        }
    }
}