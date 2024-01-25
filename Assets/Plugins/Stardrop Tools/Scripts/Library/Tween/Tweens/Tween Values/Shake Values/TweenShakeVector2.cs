
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Set Intensity before play
    /// </summary>
    public class TweenShakeVector2 : TweenVector2
    {
        protected Vector2 intensityVector2;

        protected override void SetEssentials()
        {
            tweenType = TweenType.ShakeVector2;
        }

        public TweenShakeVector2()
        {
            SetEssentials();
        }

        public TweenShakeVector2(float intensity)
        {
            SetEssentials();
            SetIntensity(intensity);
        }

        public TweenShakeVector2(Vector2 intensityVector)
        {
            SetEssentials();
            SetIntensity(intensityVector);
        }



        public TweenShakeVector2 SetIntensity(Vector2 intensity)
        {
            this.intensityVector2 = intensity;
            return this;
        }

        public TweenShakeVector2 SetIntensity(float intensityMultiplier)
        {
            intensityVector2 = Vector2.one * intensityMultiplier;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                lerped = start;

            percent = 1 - percent;

            Vector2 amount = intensityVector2 * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);

            lerped = amount + start;
        }
    }
}