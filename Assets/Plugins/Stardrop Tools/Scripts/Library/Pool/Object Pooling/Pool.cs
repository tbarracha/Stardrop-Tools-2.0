
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class Pool : MonoBehaviour, IPool
    {
        [NaughtyAttributes.ShowAssetPreview]
        [SerializeField] GameObject prefab;
        [SerializeField] int capacity = 0;
        [SerializeField] bool debug;

        Transform self;
        bool isPopulated = false;
        bool isTrimming = false;

        private readonly HashSet<PoolItem> activeCache = new HashSet<PoolItem>();
        private readonly Queue<PoolItem> pool = new Queue<PoolItem>();

        public GameObject Prefab => prefab;

        [NaughtyAttributes.ShowNativeProperty]
        public int PoolCount => pool.Count;
        [NaughtyAttributes.ShowNativeProperty]
        public int ActiveCount => activeCache.Count;

        public void SetPoolData(GameObject prefab, int capacity, bool shouldInitialize)
        {
            isPopulated = false;
            this.prefab = prefab;
            this.capacity = capacity;

            if (shouldInitialize)
                Populate();
        }
        
        public void Populate()
        {
            if (isPopulated || prefab == null)
                return;

            self = transform;
            pool.Clear();
            activeCache.Clear();

            for (int i = 0; i < capacity; i++)
                pool.Enqueue(CreateItem());

            isPopulated = true;
        }

        PoolItem CreateItem()
        {
            if (prefab == null)
            {
                if (debug)
                    Debug.Log($"<color=orange>Prefab is null!</color>");
                return null;
            }

            GameObject obj = Instantiate(prefab, self);
            obj.name += $" - {pool.Count}";
            obj.SetActive(false);

            return new PoolItem(obj, this);
        }

        public PoolItem Spawn(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
        {
            PoolItem item = pool.Count > 0 ? pool.Dequeue() : CreateItem();

            item.SetPositionRotationAndParent(position, rotation, parent);
            item.SetActive(true);
            activeCache.Add(item);

            if (lifetime > 0)
            {
                item.StartLifetimeTimer(lifetime);
            }

            item.OnSpawn();
            return item;
        }

        public TComponent Spawn<TComponent>(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
        {
            PoolItem poolItem = Spawn(position, rotation, parent, lifetime);
            return poolItem.GetComponent<TComponent>();
        }

        public TComponent Spawn<TComponent>(Vector3 position, Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
            => Spawn<TComponent>(position, Quaternion.identity, parent, lifetime);

        public TComponent Spawn<TComponent>(Vector3 position, float lifetime = 0) where TComponent : MonoBehaviour
            => Spawn<TComponent>(position, Quaternion.identity, null, lifetime);

        public TComponent Spawn<TComponent>(Transform parent, float lifetime = 0) where TComponent : MonoBehaviour
            => Spawn<TComponent>(Vector3.zero, Quaternion.identity, parent, lifetime);

        public TComponent Spawn<TComponent>(float lifetime = 0) where TComponent : MonoBehaviour
            => Spawn<TComponent>(Vector3.zero, Quaternion.identity, null, lifetime);

        public bool Despawn(PoolItem item)
        {
            if (item.IsFromPool(this) && activeCache.Remove(item))
            {
                item.OnDespawn();
                item.SetActive(false);
                item.SetParent(self);
                pool.Enqueue(item);
                return true;
            }

            if (debug)
                Debug.Log($"Object: {item.ItemGameObject.name}, didn't come from pool: {name}");
            return false;
        }

        public void DespawnAll()
        {
            if (isTrimming)
                return;

            var itemsToDespawn = new List<PoolItem>(activeCache);

            foreach (var item in itemsToDespawn)
            {
                Despawn(item);
            }

            if (ActiveCount > 0)
            {
                DespawnAll();
            }
        }

        public void TrimPool()
        {
            isTrimming = true;
            DespawnAll();

            while (pool.Count > capacity)
            {
                var item = pool.Dequeue();
                Destroy(item.ItemGameObject);
            }

            isTrimming = false;
        }

        public static void DespawnIPoolables<T>(IEnumerable<T> pooledItems) where T : IPoolable
        {
            foreach (var item in pooledItems)
            {
                item.Despawn();
            }
        }
    }
}