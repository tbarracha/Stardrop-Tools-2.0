
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
        bool isTrimming = false;

        private readonly HashSet<IPoolableObject> activeCache = new HashSet<IPoolableObject>();
        private readonly Queue<IPoolableObject> pool = new Queue<IPoolableObject>();

        public GameObject Prefab => prefab;

        [NaughtyAttributes.ShowNativeProperty]
        public int PoolCount => pool.Count;
        [NaughtyAttributes.ShowNativeProperty]
        public int ActiveCount => activeCache.Count;
        public bool IsPopulated { get; private set; }



        // Initialization
        // ---------------------------------------------------------------------------------------
        public void SetPoolData(GameObject prefab, int capacity, bool shouldInitialize)
        {
            IsPopulated = false;
            this.prefab = prefab;
            this.capacity = capacity;
            self = transform;

            if (shouldInitialize)
                Populate();
        }
        
        public void Populate()
        {
            if (IsPopulated || prefab == null)
                return;

            self = transform;
            pool.Clear();
            activeCache.Clear();

            for (int i = 0; i < capacity; i++)
                pool.Enqueue(CreateItem());

            IsPopulated = true;
        }

        IPoolableObject CreateItem()
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

            return new PoolInfo(obj, this).PoolableObject;
        }



        // Spawn
        // ---------------------------------------------------------------------------------------
        public IPoolableObject Spawn(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0)
        {
            IPoolableObject item = pool.Count > 0 ? pool.Dequeue() : CreateItem();

            // Cast to IPoolInfo to access SetPositionRotationAndParent, SetActive, etc.
            IPoolInfo poolInfo = item.PoolInfo;
            poolInfo.SetPositionRotationAndParent(position, rotation, parent);
            poolInfo.SetActive(true);

            activeCache.Add(item);

            if (lifetime > 0)
            {
                poolInfo.StartLifetimeTimer(lifetime);
            }

            item.OnSpawn();
            return item;
        }

        public TComponent Spawn<TComponent>(Vector3 position, Quaternion rotation, Transform parent, float lifetime = 0) where TComponent : Component
        {
            IPoolableObject item = Spawn(position, rotation, parent, lifetime);
            return item.PoolInfo?.GetComponent<TComponent>();
        }

        public TComponent Spawn<TComponent>(Vector3 position, Transform parent, float lifetime = 0) where TComponent : Component
            => Spawn<TComponent>(position, Quaternion.identity, parent, lifetime);

        public TComponent Spawn<TComponent>(Vector3 position, float lifetime = 0) where TComponent : Component
            => Spawn<TComponent>(position, Quaternion.identity, null, lifetime);

        public TComponent Spawn<TComponent>(Transform parent, float lifetime = 0) where TComponent : Component
            => Spawn<TComponent>(Vector3.zero, Quaternion.identity, parent, lifetime);

        public TComponent Spawn<TComponent>(float lifetime = 0) where TComponent : Component
            => Spawn<TComponent>(Vector3.zero, Quaternion.identity, null, lifetime);



        // Despawn
        // ---------------------------------------------------------------------------------------
        public bool Despawn(IPoolableObject pooledObject)
        {
            IPoolInfo poolInfo = pooledObject.PoolInfo;
            if (poolInfo.IsFromPool(this) && activeCache.Remove(pooledObject))
            {
                if (self == null)
                    self = transform;

                pooledObject.OnDespawn();
                poolInfo.SetParent(self);
                poolInfo.SetActive(false);
                pool.Enqueue(pooledObject);
                

                return true;
            }

            if (debug)
                Debug.Log($"Object: {poolInfo.PrefabGameObject.name}, didn't come from pool: {name}");

            return false;
        }

        public bool Despawn(IPoolInfo poolInfo)
        {
            IPoolableObject poolableObject = poolInfo.PoolableObject;
            if (poolInfo.IsFromPool(this) && activeCache.Remove(poolableObject))
            {
                if (self == null)
                    self = transform;

                poolableObject.OnDespawn();
                poolInfo.SetParent(self);
                poolInfo.SetActive(false);
                pool.Enqueue(poolableObject);

                return true;
            }

            if (debug)
                Debug.Log($"Object: {poolInfo.PrefabGameObject.name}, didn't come from pool: {name}");

            return false;
        }

        public void DespawnAll()
        {
            if (isTrimming)
                return;

            var itemsToDespawn = new List<IPoolableObject>(activeCache);

            foreach (var item in itemsToDespawn)
            {
                Despawn(item);
            }

            if (ActiveCount > 0)
            {
                DespawnAll();
            }
        }



        // Trim
        // ---------------------------------------------------------------------------------------
        public void TrimPool()
        {
            isTrimming = true;
            DespawnAll();

            while (pool.Count > capacity)
            {
                IPoolableObject item = pool.Dequeue();
                Destroy(item.PoolInfo.PrefabGameObject);
            }

            isTrimming = false;
        }
    }
}