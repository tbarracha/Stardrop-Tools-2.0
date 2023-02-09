
namespace StardropTools.Pool
{
    public interface IPoolable
    {
        public void OnSpawn();
        public void OnDespawn();
        public void Despawn();
        public void SetPoolItem(PoolItem poolItem);
    }
}