
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenTransformComponent : TweenComponent
    {
        [Header("Target Transform")]
        public SimulationSpace simulationSpace;
        public Transform target;

        [NaughtyAttributes.Button("Get Self Transform")]
        private void GetTransform()
        {
            target = transform;
        }
    }
}