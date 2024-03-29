﻿
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolCluster : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea(1, 5)] [SerializeField] string description;
        [SerializeField] List<GameObject> prefabsToPool;
        [Space]
#endif

        [SerializeField] List<IPool> pools;

        public void Populate()
        {
            GetPools();

            for (int i = 0; i < pools.Count; i++)
                pools[i].Populate();
        }



        public PoolItem SpawnFromPool(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
            => pools[poolIndex].Spawn(position, rotation, parent, lifetime);


        public TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
            => pools[poolIndex].Spawn<TComponent>(position, rotation, parent, lifetime);

        public TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
            => pools[poolIndex].Spawn<TComponent>(position, Quaternion.identity, parent, lifetime);

        public TComponent SpawnFromPool<TComponent>(int poolIndex, Vector3 position, float lifetime = 0) where TComponent : MonoBehaviour
            => pools[poolIndex].Spawn<TComponent>(position, Quaternion.identity, null, lifetime);

        public TComponent SpawnFromPool<TComponent>(int poolIndex, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
            => pools[poolIndex].Spawn<TComponent>(Vector3.zero, Quaternion.identity, parent, lifetime);

        public TComponent SpawnFromPool<TComponent>(int poolIndex, float lifetime = 0) where TComponent : MonoBehaviour
            => pools[poolIndex].Spawn<TComponent>(Vector3.zero, Quaternion.identity, null, lifetime);



        public bool DespawnToPool(int poolIndex, PoolItem item) => pools[poolIndex].Despawn(item);


        public void ClearPools()
        {
            for (int i = 0; i < pools.Count; i++)
                pools[i].DespawnAll();
        }


        public void ClearPool(int poolIndex) => pools[poolIndex].DespawnAll();


        public void CreatePool(GameObject prefab)
        {
            GameObject poolObj = Utilities.CreateEmpty("Pool - " + prefab.name, Vector3.zero, transform).gameObject;
            Pool pool = poolObj.AddComponent<Pool>();
            pool.SetPoolData(prefab, 1, false);
        }


        [NaughtyAttributes.Button("Get Child Pools")]
        void GetPools()
        {
            Pool[] childPools = GetComponentsInChildren<Pool>();
            if (childPools.Exists() == false)
            {
                Debug.Log($"<color=yellow>No pool children found in:</color> <color=white>{name}</color>");
                return;
            }

            pools = new List<IPool>();

            for (int i = 0; i < childPools.Length; i++)
                pools.Add(childPools[i]);
        }


#if UNITY_EDITOR
        [NaughtyAttributes.Button("Create Pools from Prefabs")]
        void CreatePoolsFromPrefabs()
        {
            if (prefabsToPool.Count == 0)
            {
                Debug.Log($"<color=cyan>No prefabs to pool in:</color> <color=white>{name}</color>");
                return;
            }

            bool exists = false;
            for (int i = 0; i < prefabsToPool.Count; i++)
            {
                GameObject prefab = prefabsToPool[i];

                for (int j = 0; j < pools.Count; j++)
                {
                    if (prefab.name == pools[j].Prefab.name)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                    CreatePool(prefab);
            }

            prefabsToPool = new List<GameObject>();
            GetPools();
        }
#endif
    }
}