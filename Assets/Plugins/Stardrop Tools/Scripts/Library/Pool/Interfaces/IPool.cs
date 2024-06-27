
using UnityEngine;

namespace StardropTools.Pool
{
    public interface IPool
    {
        public GameObject Prefab { get; }
        public int PoolCount { get; }
        public int ActiveCount { get; }

        public void Populate();
        public void SetPoolData(GameObject prefab, int capacity, bool shouldInitialize);

        public IPoolableObject Spawn(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0);
        public TComponent Spawn<TComponent>(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : Component;

        public bool Despawn(IPoolableObject pooledObject);
        public bool Despawn(IPoolInfo poolInfo);
        public void DespawnAll();
        
        public void TrimPool();
    }
}
