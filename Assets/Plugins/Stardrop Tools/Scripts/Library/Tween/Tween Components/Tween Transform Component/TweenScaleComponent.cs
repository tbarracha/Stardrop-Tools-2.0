
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenScaleComponent : TweenTransformComponent
    {
        public Vector3 startScale;
        public Vector3 endScale;
        [SerializeField] bool uniform;

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenLocalScale(target, startScale, endScale);
            else
                tween = new TweenLocalScale(target, endScale);

            SetTweenEssentials();
            tween.SetID(this).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Scale")]
        private void GetStart()
        {
            startScale = target.localScale;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (simulationSpace != SimulationSpace.LocalSpace)
                simulationSpace = SimulationSpace.LocalSpace;

            if (uniform)
                endScale = new Vector3(endScale.x, endScale.x, endScale.x);
        }
#endif
    }
}