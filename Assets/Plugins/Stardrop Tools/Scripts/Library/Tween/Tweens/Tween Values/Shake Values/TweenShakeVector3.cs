
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before play
    /// </summary>
    public class TweenShakeVector3 : TweenVector3
    {
        protected Vector3 intensityVector4;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeVector3;
        }

        public TweenShakeVector3()
        {
            SetEssentials();
        }

        public TweenShakeVector3(float intensity)
        {
            SetEssentials();
            SetIntensity(intensity);
        }

        public TweenShakeVector3(Vector3 intensityVector)
        {
            SetEssentials();
            SetIntensity(intensityVector);
        }

        public TweenShakeVector3 SetIntensity(Vector3 intensity)
        {
            this.intensityVector4 = intensity;
            return this;
        }

        public TweenShakeVector3 SetIntensity(float intensityMultiplier)
        {
            intensityVector4 = Vector3.one * intensityMultiplier;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            Vector3 amount = intensityVector4 * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);

            lerped = amount + start;
        }

        protected override void Complete()
        {
            base.Complete();
            lerped = end;
        }
    }
}