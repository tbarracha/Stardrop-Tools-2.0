using StardropTools.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class LifetimeEffect : BaseComponent, IPlayableCallback, IStoppable, IPoolableObject
    {
        [SerializeField] float lifeTime = 0;
        [SerializeField] List<ParticleSystem> particleList;
        [SerializeField] List<TrailRenderer> trailList;

        System.Action onPlayCallback;
        Timer lifeTimer;

        [NaughtyAttributes.Button("Play")]
        public void Play()
        {
            Clear();
            Activate();

            if (lifeTime > 0)
            {
                lifeTimer?.Stop();
                lifeTimer = new Timer(lifeTime).Play(Stop);
            }

            PlayEffects();
        }

        public void Play(System.Action onPlayCallback)
        {
            this.onPlayCallback = onPlayCallback;
            Play();
        }

        [NaughtyAttributes.Button("Stop")]
        public void Stop()
        {
            if (onPlayCallback != null)
            {
                onPlayCallback.Invoke();
                onPlayCallback = null;
            }

            StopEffects();
            lifeTimer = null;

            Deactivate();
        }

        public void Clear()
        {
            Utilities.ClearParticles(particleList);
            Utilities.ClearTrails(trailList);
        }

        protected void PlayEffects()
        {
            if (particleList.Exists())
                Utilities.PlayParticles(particleList);

            if (trailList.Exists())
                Utilities.PlayTrails(trailList);
        }

        protected void StopEffects()
        {
            if (particleList.Exists())
                Utilities.StopParticles(particleList);

            if (trailList.Exists())
                Utilities.StopTrails(trailList);
        }

        [NaughtyAttributes.Button("Get Effects")]
        public void GetEffects()
        {
            particleList = GetComponentsInChildren<ParticleSystem>().ToList() ?? new List<ParticleSystem>();
            trailList = GetComponentsInChildren<TrailRenderer>().ToList() ?? new List<TrailRenderer>();
        }

        [System.Obsolete]
        public void SetParticleLifeTime(int psIndex, float lifeTime)
        {
            if (psIndex > 0 && psIndex < particleList.Count)
            {
                var ps = particleList[psIndex];
                ps.startLifetime = lifeTime;
            }
        }

        #region Poolable

        public PoolInfo PoolInfo { get; protected set; }

        public void SetPoolInfo(PoolInfo poolInfo) => this.PoolInfo = poolInfo;

        public void Despawn()
        {
            if (PoolInfo != null)
                PoolInfo.Despawn();
            else
                Destroy(PoolInfo.PrefabGameObject);
        }

        public virtual void OnSpawn()
        {
            Play();
        }

        public virtual void OnDespawn()
        {
            
        }

        #endregion // Poolable
    }
}