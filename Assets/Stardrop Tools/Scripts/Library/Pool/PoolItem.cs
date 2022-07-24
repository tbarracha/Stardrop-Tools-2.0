
using UnityEngine;

namespace StardropTools.Pool
{
    public struct PoolItem<T> where T : Component
    {
        public int InstanceID { get; private set; }
        public Pool<T> OriginPool { get; private set; }
        public T Component { get; private set; }
        public IPoolable<T> Poolable { get; private set; }
        public GameObject GameObject { get; private set; }
        public Transform Transform { get; private set; }

        public bool IsActive => GameObject.activeInHierarchy;

        public PoolItem(Pool<T> pool, IPoolable<T> poolable, GameObject instance, int instanceID)
        {
            OriginPool = pool;
            InstanceID = instanceID;

            Component = instance.GetComponent<T>();
            Poolable = poolable;

            GameObject = Component.gameObject;
            Transform = Component.transform;
        }

        public void SetActive(bool value)
            => GameObject.SetActive(value);

        public void SetPosition(Vector3 position)
            => Transform.position = position;

        public void SetRotation(Quaternion rotation)
            => Transform.rotation = rotation;

        public void SetParent(Transform parent)
            => Transform.parent = parent;

        public void SetTransforms(Vector3 position, Quaternion rotation, Transform parent)
        {
            SetPosition(position);
            SetRotation(rotation);
            SetParent(parent);
        }

        public void Despawn() => OriginPool.Despawn(this);
    }
}