
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeRotationComponent : TweenShakeTransformComponent
    {
        public override Tween Play()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                tween = new TweenShakeEulerRotation(target, target.eulerAngles);

            if (simulationSpace == SimulationSpace.LocalSpace)
                tween = new TweenShakeLocalEulerRotation(target, target.localEulerAngles);

            SetTweenEssentials();
            tween.Play();

            return tween;
        }
    }
}