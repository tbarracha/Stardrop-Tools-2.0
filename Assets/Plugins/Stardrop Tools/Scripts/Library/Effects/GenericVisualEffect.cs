
using UnityEngine;

namespace StardropTools
{
    public class GenericVisualEffect : VisualEffect
    {
        [SerializeField] private ParticleSystem[] particleSystems;
        [SerializeField] private TrailRenderer[]  trailRenderers;

        protected override void OnPlay()
        {
            base.OnPlay();

            Utilities.PlayParticles(particleSystems);
            Utilities.PlayTrails(trailRenderers);
        }

        protected override void OnStop()
        {
            base.OnStop();

            ResetComponent();
        }

        public override void ResetComponentPublic() => ResetComponent();

        protected override void ResetComponent()
        {
            Utilities.ClearParticles(particleSystems);
            Utilities.ClearTrails(trailRenderers);
        }

        [NaughtyAttributes.Button("Get Effects")]
        public void GetEffects()
        {
            particleSystems = GetComponentsInChildren<ParticleSystem>();
            trailRenderers = GetComponentsInChildren<TrailRenderer>();
        }

        #region Poolable

        public override void OnSpawn()
        {
            Play();
        }

        public override void OnDespawn()
        {
            Stop();
        }

        #endregion // Poolable
    }
}