using UnityEngine;

namespace StardropTools.Pool
{
    public interface IPoolCluster
    {
        // Method to populate all pools
        void Populate();

        // Methods to spawn objects from a specific pool
        IPoolableObject SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0);
        TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : Component;
        TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Transform parent, float lifetime = 0) where TComponent : Component;
        TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, float lifetime = 0) where TComponent : Component;
        TComponent SpawnFromPool<TComponent>(int poolIndex, Transform parent, float lifetime = 0) where TComponent : Component;
        TComponent SpawnFromPool<TComponent>(int poolIndex, float lifetime = 0) where TComponent : Component;

        // Method to despawn an item back to a specific pool
        bool DespawnToPool(int poolIndex, IPoolableObject poolableItem);
        bool DespawnToPool(int poolIndex, IPoolInfo poolInfo);

        // Methods to clear one or all pools
        void DespawnAllPools();
        void DespawnPool(int poolIndex);

        // Method to create a new pool
        void CreatePool(GameObject prefab);
    }
}
