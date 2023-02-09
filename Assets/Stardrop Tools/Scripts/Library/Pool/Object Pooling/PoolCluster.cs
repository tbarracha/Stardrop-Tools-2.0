
namespace StardropTools.Pool
{

    using System.Collections.Generic;
    using UnityEngine;

    public class PoolCluster : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea(1, 5)] [SerializeField] string description;
#endif
        [SerializeField] List<Pool> pools;
        [SerializeField] bool getPools;

        public void Populate()
        {
            GetPools();

            for (int i = 0; i < pools.Count; i++)
                pools[i].Populate();
        }



        public PoolItem SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
        {
            PoolItem item = pools[poolIndex].Spawn(position, rotation, parent, lifetime);
            return item;
        }


        public T SpawnFromPool<T>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
            => pools[poolIndex].Spawn<T>(position, rotation, parent, lifetime);



        public bool DespawnToPool(int poolIndex, PoolItem item) => pools[poolIndex].Despawn(item);


        public void DespawnAllPools()
        {
            for (int i = 0; i < pools.Count; i++)
                pools[i].DespawnAll();
        }

        void GetPools()
        {
            Pool[] childPools = GetComponentsInChildren<Pool>();
            pools = new List<Pool>();

            for (int i = 0; i < childPools.Length; i++)
                pools.Add(childPools[i]);
        }

        private void OnValidate()
        {
            if (getPools)
            {
                GetPools();
                getPools = false;
            }
        }
    }
}