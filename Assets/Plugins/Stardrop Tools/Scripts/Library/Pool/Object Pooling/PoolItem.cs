namespace StardropTools.Pool
{
    using System.Collections;
    using UnityEngine;

    [System.Serializable]
    public class PoolItem
    {
        [SerializeField] private Pool pool;
        [SerializeField] private GameObject itemObject;

        private IPoolable poolable;
        private MonoBehaviour cachedComponent;
        private Timer lifetimeTimer;

        public GameObject ItemGameObject => itemObject;
        public Transform ItemTransform => itemObject.transform;
        public bool IsActive { get; private set; }

        public PoolItem(GameObject prefab, Pool pool)
        {
            this.pool = pool;
            itemObject = prefab;
            poolable = prefab.GetComponent<IPoolable>();
            IsActive = false;
            prefab.SetActive(false);

            if (poolable != null)
                poolable.SetPoolItem(this);
        }

        public void OnSpawn()
        {
            if (poolable != null)
                poolable.OnSpawn();
        }

        public void OnDespawn()
        {
            if (poolable != null)
                poolable.OnDespawn();

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
            ItemTransform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            ItemTransform.rotation = rotation;
        }

        public void SetParent(Transform parent)
        {
            ItemTransform.SetParent(parent);
        }

        public void SetPositionRotationAndParent(Vector3 position, Quaternion rotation, Transform parent)
        {
            SetPosition(position);
            SetRotation(rotation);
            SetParent(parent);
        }

        public TComponent GetComponent<TComponent>() where TComponent : MonoBehaviour
        {
            if (cachedComponent == null || cachedComponent is not TComponent)
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
