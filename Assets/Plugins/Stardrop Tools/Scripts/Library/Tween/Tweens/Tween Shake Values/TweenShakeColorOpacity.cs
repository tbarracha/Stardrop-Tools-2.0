using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeColorOpacity : TweenShakeFloat
    {
        protected Color color;
        
        public TweenShakeColorOpacity(Color color, float startValue, float endValue, float intensity) : base(startValue, endValue, intensity)
        {
            this.color = color;
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
