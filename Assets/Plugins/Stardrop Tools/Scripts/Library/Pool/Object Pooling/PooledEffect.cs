
using UnityEngine;

namespace StardropTools.Pool
{
    public class PooledEffect : BaseTransform, IPoolable
    {
        [SerializeField] float lifeTime = 0;
        [SerializeField] ParticleSystem[]   particles;
        [SerializeField] TrailRenderer[]    trails;


        #region Poolable

        public PoolItem PoolItem { get; protected set; }

        public void SetPoolItem(PoolItem poolItem) => this.PoolItem = poolItem;

        public void Despawn()
        {
            if (PoolItem != null)
                PoolItem.Despawn();
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