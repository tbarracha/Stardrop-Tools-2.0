
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakePositionComponent : TweenShakeTransformComponent
    {
        public override Tween Play()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                tween = new TweenShakePosition(target, target.position);

            if (simulationSpace == SimulationSpace.LocalSpace)
                tween = new TweenShakeLocalPosition(target, target.localPosition);

            SetTweenEssentials();
            tween.Play();

            return tween;
        }
    }
}