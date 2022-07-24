

namespace StardropTools.Pool
{
    public interface IPoolable<T> where T : UnityEngine.Component
    {
        public void SetPoolItem(PoolItem<T> item);

        public void OnSpawn();
        public void OnDespawn();

        public void Despawn();
    }
}