using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeVector4 : TweenVector4
    {
        protected Vector4 intensity;

        public TweenShakeVector4(Vector4 endValue, Vector4 intensity) : base(endValue)
        {
            this.intensity = intensity;
        }
        
        public TweenShakeVector4(Vector4 startValue, Vector4 endValue, Vector4 intensity) : base(startValue, endValue)
        {
            this.intensity = intensity;
        }


        // float intensity
        public TweenShakeVector4(Vector4 endValue, float intensity) : base(endValue)
        {
            this.intensity = Vector4.one * intensity;
        }

        public TweenShakeVector4(Vector4 startValue, Vector4 endValue, float intensity) : base(startValue, endValue)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector4;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector4 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);
            amount.w = Random.Range(-amount.w, amount.w);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
