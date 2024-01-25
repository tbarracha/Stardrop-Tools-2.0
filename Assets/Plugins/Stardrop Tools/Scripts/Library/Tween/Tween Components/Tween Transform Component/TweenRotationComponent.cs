
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotationComponent : TweenTransformComponent
    {
        public Vector3 startRotation;
        public Vector3 endRotation;

        public override Tween Play()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenEulerRotation(target, startRotation, endRotation);
                else
                    tween = new TweenEulerRotation(target, endRotation);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenLocalEulerRotation(target, startRotation, endRotation);
                else
                    tween = new TweenLocalEulerRotation(target, endRotation);
            }

            SetTweenEssentials();
            tween.SetLoopType(loopType);
            tween.Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Rotation")]
        private void GetStart()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                startRotation = target.eulerAngles;
            else
                startRotation = target.localEulerAngles;
        }
    }
}