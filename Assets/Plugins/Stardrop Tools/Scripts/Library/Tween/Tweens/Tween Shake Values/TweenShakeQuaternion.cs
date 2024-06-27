using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeQuaternion : TweenQuaternion
    {
        protected Vector4 intensity;

        public TweenShakeQuaternion(Quaternion endValue, Vector4 intensity) : base(endValue)
        {
            this.intensity = intensity;
        }
        
        public TweenShakeQuaternion(Quaternion startValue, Quaternion endValue, Vector4 intensity) : base(startValue, endValue)
        {
            this.intensity = intensity;
        }


        // float intensity
        public TweenShakeQuaternion(Quaternion endValue, float intensity) : base(endValue)
        {
            this.intensity = Vector4.one * intensity;
        }

        public TweenShakeQuaternion(Quaternion startValue, Quaternion endValue, float intensity) : base(startValue, endValue)
        {
            this.intensity = Vector4.one * intensity;
        }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakeQuaternion;
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

            Lerped = Quaternion.Euler(amount) * StartValue;

            OnTweenValue?.Invoke(Lerped);
        }
    }
}
