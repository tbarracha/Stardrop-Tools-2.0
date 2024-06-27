

namespace StardropTools.Pool
{
    public interface IPoolableObject : IPoolable
    {
        public PoolInfo PoolInfo { get; }
        public void SetPoolInfo(PoolInfo poolInfo);

        /*
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

        }

        public virtual void OnDespawn()
        {

        }

        #endregion // Poolable
        */
    }
}