using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeVector3 : TweenVector3
    {
        protected Vector3 intensity;

        public TweenShakeVector3(Vector3 endValue, Vector3 intensity) : base(endValue)
        {
            this.intensity = intensity;
        }
        
        public TweenShakeVector3(Vector3 startValue, Vector3 endValue, Vector3 intensity) : base(startValue, endValue)
        {
            this.intensity = intensity;
        }


        // float intensity
        public TweenShakeVector3(Vector3 endValue, float intensity) : base(endValue)
        {
            this.intensity = Vector3.one * intensity;
        }

        public TweenShakeVector3(Vector3 startValue, Vector3 endValue, float intensity) : base(startValue, endValue)
        {
            this.intensity = Vector3.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeVector3;
        }

        protected override void TweenUpdate(float percent)
        {
            if (percent == 0)
                Lerped = StartValue;

            percent = 1 - percent;

            Vector3 amount = intensity * Ease(percent);
            amount.x = Random.Range(-amount.x, amount.x);
            amount.y = Random.Range(-amount.y, amount.y);
            amount.z = Random.Range(-amount.z, amount.z);

            Lerped = StartValue + amount;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
