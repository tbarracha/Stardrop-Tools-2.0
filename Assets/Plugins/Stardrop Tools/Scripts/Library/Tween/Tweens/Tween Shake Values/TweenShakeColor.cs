using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeColor : TweenColor
    {
        protected Color intensity;
        protected bool shakeOpacity;

        public TweenShakeColor(Color endValue, Color intensity, bool shakeOpacity) : base(endValue)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }
        
        public TweenShakeColor(Color startValue, Color endValue, Color intensity, bool shakeOpacity) : base(startValue, endValue)
        {
            this.intensity = intensity;
            this.shakeOpacity = shakeOpacity;
        }


        // float intensity
        public TweenShakeColor(Color endValue, float intensity, bool shakeOpacity) : base(endValue)
        {
            this.intensity = Color.white * intensity;
            this.shakeOpacity = shakeOpacity;
        }

        public TweenShakeColor(Color startValue, Color endValue, float intensity, bool shakeOpacity) : base(startValue, endValue)
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
