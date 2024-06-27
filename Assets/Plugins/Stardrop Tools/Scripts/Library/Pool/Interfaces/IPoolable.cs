
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public void OnSpawn();
        public void OnDespawn();
        public void Despawn();

        /*
        #region Poolable

        public virtual void Despawn()
        {
            
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
