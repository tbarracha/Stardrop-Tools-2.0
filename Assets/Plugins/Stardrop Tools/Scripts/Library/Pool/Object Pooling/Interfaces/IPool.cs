
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

        public PoolItem Spawn(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0);
        public TComponent Spawn<TComponent>(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour;

        public bool Despawn(PoolItem item);
        public void DespawnAll();
        
        public void TrimPool();
    }
}
