namespace StardropTools.Pool
{
    using UnityEngine;

    [System.Serializable]
    public class PoolInfo : IPoolInfo
    {
        [SerializeField] private Pool pool;
        [SerializeField] private GameObject itemObject;

        private IPoolableObject poolableObject;
        private Component cachedComponent;
        private Timer lifetimeTimer;

        public GameObject PrefabGameObject => itemObject;
        public Transform PrefabTransform => itemObject.transform;
        public IPoolableObject PoolableObject => poolableObject;

        public bool IsActive { get; private set; }

        public PoolInfo(GameObject prefab, Pool pool)
        {
            this.pool = pool;
            itemObject = prefab;
            poolableObject = prefab.GetComponent<IPoolableObject>();
            SetActive(false);

            if (poolableObject != null)
                poolableObject.SetPoolInfo(this);
        }

        public void OnSpawn()
        {
            if (poolableObject != null)
                poolableObject.OnSpawn();
        }

        public void OnDespawn()
        {
            if (poolableObject != null)
                poolableObject.OnDespawn();

            StopLifetimeTimer();
        }

        public void Despawn()
        {
            if (pool != null)
                pool.Despawn(this);
        }

        public void SetActive(bool value)
        {
            itemObject.SetActive(value);
            IsActive = value;
        }

        public void SetPosition(Vector3 position)
        {
            PrefabTransform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            PrefabTransform.rotation = rotation;
        }

        public void SetParent(Transform parent)
        {
            PrefabTransform.SetParent(parent);
        }

        public void SetPositionRotationAndParent(Vector3 position, Quaternion rotation, Transform parent)
        {
            SetPosition(position);
            SetRotation(rotation);
            SetParent(parent);
        }

        public TComponent GetComponent<TComponent>() where TComponent : Component
        {
            if (cachedComponent is not TComponent)
            {
                cachedComponent = itemObject.GetComponent<TComponent>();
            }

            return cachedComponent as TComponent;
        }

        public bool IsFromPool(Pool targetPool)
        {
            return pool == targetPool;
        }

        private void StopLifetimeTimer()
        {
            lifetimeTimer?.Stop();
            lifetimeTimer = null;
        }

        public void StartLifetimeTimer(float lifetime)
        {
            StopLifetimeTimer();
            lifetimeTimer = new Timer(lifetime).Play(Despawn);
        }
    }
}
