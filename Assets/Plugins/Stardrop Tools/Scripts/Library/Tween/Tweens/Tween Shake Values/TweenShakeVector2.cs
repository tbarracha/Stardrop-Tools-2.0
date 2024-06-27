using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeVector2 : TweenVector2
    {
        protected Vector2 intensity;

        public TweenShakeVector2(Vector2 endValue, Vector2 intensity) : base(endValue)
        {
            this.intensity = intensity;
        }
        
        public TweenShakeVector2(Vector2 startValue, Vector2 endValue, Vector2 intensity) : base(startValue, endValue)
        {
            this.intensity = intensity;
        }


        // float intensity
        public TweenShakeVector2(Vector2 endValue, float intensity) : base(endValue)
        {
            this.intensity = Vector2.one * intensity;
        }

        public TweenShakeVector2(Vector2 startValue, Vector2 endValue, float intensity) : base(startValue, endValue)
        {
            this.intensity = Vector2.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector2;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector2 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
