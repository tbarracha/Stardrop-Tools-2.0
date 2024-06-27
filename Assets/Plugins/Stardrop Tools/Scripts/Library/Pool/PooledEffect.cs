using UnityEngine;

namespace StardropTools.Pool
{
    public class PooledEffect : BaseTransform, IPoolableObject
    {
        [SerializeField] float lifeTime = 0;
        [SerializeField] ParticleSystem[]   particles;
        [SerializeField] TrailRenderer[]    trails;


        #region Poolable

        public PoolInfo PoolInfo { get; protected set; }

        public void SetPoolInfo(PoolInfo poolItem) => this.PoolInfo = poolItem;

        public void Despawn()
        {
            if (PoolInfo != null)
                PoolInfo.Despawn();
            else
                Destroy(thisObject);
        }

        public void OnSpawn()
        {
            Utilities.ClearParticles(particles);
            Utilities.ClearTrails(trails);

            if (lifeTime > 0)
                Invoke(nameof(Despawn), lifeTime);
        }

        public void OnDespawn()
        {
            Utilities.ClearParticles(particles);
            Utilities.ClearTrails(trails);
        }

        #endregion // Poolable


        [NaughtyAttributes.Button("Get Components")]
        void GetComponents()
        {
            particles = GetComponentsInChildren<ParticleSystem>();
            trails = GetComponentsInChildren<TrailRenderer>();
        }
    }
}