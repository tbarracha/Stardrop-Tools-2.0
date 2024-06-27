
using UnityEngine;

namespace StardropTools.Tween.Components
{
    public class TweenShakeScaleComponent : TweenComponentTransform<Vector3>
    {
        [Header("Shake Intensity")]
        [SerializeField] float intensity;

        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenShakeLocalScale(startValue, endValue, intensity, targets);
            else
                tween = new TweenShakeLocalScale(endValue, intensity, targets);

            SetTweenEssentials();
        }
    }
}