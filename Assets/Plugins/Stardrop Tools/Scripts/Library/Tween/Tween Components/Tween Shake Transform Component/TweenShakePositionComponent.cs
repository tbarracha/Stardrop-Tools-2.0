
using UnityEngine;

namespace StardropTools.Tween.Components
{
    public class TweenShakePositionComponent : TweenComponentTransform<Vector3>
    {
        [Header("Shake Intensity")]
        [SerializeField] float intensity;

        protected override void CreateTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenShakePosition(startValue, endValue, intensity, targets);
                else
                    tween = new TweenShakePosition(endValue, intensity, targets);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenShakeLocalPosition(startValue, endValue, intensity, targets);
                else
                    tween = new TweenShakeLocalPosition(endValue, intensity, targets);
            }

            SetTweenEssentials();
        }
    }
}