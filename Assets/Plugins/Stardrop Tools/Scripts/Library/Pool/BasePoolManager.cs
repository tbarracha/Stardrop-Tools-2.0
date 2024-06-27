using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Pool
{
    public class BasePoolManager<T> : BaseComponent where T : MonoBehaviour
    {
        #region Singleton
        [SerializeField] protected bool isSingleton;

        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }

        void SingletonInitialization()
        {
            if (isSingleton == false)
                return;

            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }

            //else
            //    Destroy(gameObject);
        }
        #endregion // singleton


        [SerializeField] protected Pool[] pools;
        [SerializeField] protected PoolCluster[] poolClusters;

        public bool IsPopulated { get; protected set; } = false;


        protected override void Awake()
        {
            base.Awake();
            SingletonInitialization();
            PopulatePools();
        }

        public void PopulatePools()
        {
            if (IsPopulated)
                return;

            GetPools();

            for (int i = 0; i < pools.Length; i++)
                pools[i].Populate();

            for (int i = 0; i < poolClusters.Length; i++)
                poolClusters[i].Populate();

            BaseGameEventManager.InitializationEvents.OnPoolsInitialized?.Invoke();
            IsPopulated = true;
        }


        protected TComponent SpawnFromPool<TComponent>(int poolID, Vector3 position, Quaternion rotation, Transform parent) where TComponent : MonoBehaviour
            => pools[poolID].Spawn<TComponent>(position, rotation, parent);
        protected TComponent SpawnFromPool<TComponent>(Pool pool, Vector3 position, Quaternion rotation, Transform parent) where TComponent : MonoBehaviour
            => pool.Spawn<TComponent>(position, rotation, parent);


        protected TComponent SpawnFromCluster<TComponent>(int clusterID, int poolID, Vector3 position, Quaternion rotation, Transform parent) where TComponent : MonoBehaviour
            => poolClusters[clusterID].SpawnFromPool<TComponent>(poolID, position, rotation, parent);
        protected TComponent SpawnFromCluster<TComponent>(PoolCluster poolCluster, int poolID, Vector3 position, Quaternion rotation, Transform parent) where TComponent : MonoBehaviour
            => poolCluster.SpawnFromPool<TComponent>(poolID, position, rotation, parent);



        protected void GetPools()
        {
            Pool[] childPools = Utilities.GetComponentArrayInChildren<Pool>(transform);
            if (childPools.Exists() == false)
            {
                Debug.Log($"<color=yellow>No pool children found in:</color> <color=white>{name}</color>");
                return;
            }

            var listPools = new List<Pool>();

            for (int i = 0; i < childPools.Length; i++)
                listPools.Add(childPools[i]);

            pools = listPools.ToArray();
        }

        protected void GetClusters()
        {
            PoolCluster[] childClusters = Utilities.GetComponentArrayInChildren<PoolCluster>(transform);
            if (childClusters.Exists() == false)
            {
                Debug.Log($"<color=yellow>No PoolCluster children found in:</color> <color=white>{name}</color>");
                return;
            }

            var listClusters = new List<PoolCluster>();

            for (int i = 0; i < childClusters.Length; i++)
                listClusters.Add(childClusters[i]);

            poolClusters = listClusters.ToArray();
        }

        [NaughtyAttributes.Button("Get Pools & Clusters")]
        protected void GetPoolsAndClusters()
        {
            GetPools();
            GetClusters();
        }
    }
}