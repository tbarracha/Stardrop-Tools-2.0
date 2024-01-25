
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public PoolItem PoolItem { get; }

        public void OnSpawn();
        public void OnDespawn();
        public void Despawn();
        public void SetPoolItem(PoolItem poolItem);

        /*
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