using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    /// <summary>
    /// Group of several Item Pools that inherit from the same Component.
    /// <para> Useful when spawning derived/inherited components of the main Component. Ex: enemies, bullets, spells </para>
    /// </summary>
    [System.Serializable]
    public class PoolCluster<T> where T : Component
    {
        [SerializeField] GameObject[] objectsToPool;
        [SerializeField] List<Pool<T>> pools;

        bool isPopulated;

        public PoolCluster(Transform parent, GameObject[] prefabs, int capacity, bool populate = true)
        {
            pools = new List<Pool<T>>();

            for (int i = 0; i < prefabs.Length; i++)
            {
                Transform poolParent = Utilities.CreateEmpty("Pool - " + prefabs[i].name, Vector3.zero, parent);
                Pool<T> pool = new Pool<T>(prefabs[i], capacity, poolParent, false);

                pools.Add(pool);
            }

            if (populate)
                PopulateCluster();
        }

        public void PopulateCluster()
        {
            if (isPopulated)
                return;

            for (int i = 0; i < pools.Count; i++)
                pools[i].Populate();

            isPopulated = true;
        }


        public void SpawnFromPool(int poolIndex) => pools[poolIndex].Spawn();

        public void SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn(position, rotation, parent);



        public TComponent SpawnFromPool<TComponent>(int poolIndex)
            => pools[poolIndex].Spawn().GetComponent<TComponent>();

        public TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => pools[poolIndex].Spawn<TComponent>(position, rotation, parent);



        public void DespawnFromPool(int poolIndex, T item)
            => pools[poolIndex].Despawn(item);

        public void DespawnFromPool(int poolIndex, GameObject item)
            => pools[poolIndex].Despawn(item);



        public void DespawnCluster(bool clearOverflow)
        {
            for (int i = 0; i < pools.Count; i++)
                pools[i].DespawnAll(clearOverflow);
        }
    }
}