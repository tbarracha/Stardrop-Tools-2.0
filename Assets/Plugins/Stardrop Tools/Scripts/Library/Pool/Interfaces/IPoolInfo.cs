namespace StardropTools.Pool
{
    using UnityEngine;

    public interface IPoolInfo : IPoolable
    {
        GameObject PrefabGameObject { get; }
        Transform PrefabTransform { get; }
        IPoolableObject PoolableObject { get; }

        bool IsActive { get; }

        void SetActive(bool value);
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void SetParent(Transform parent);
        void SetPositionRotationAndParent(Vector3 position, Quaternion rotation, Transform parent);
        
        TComponent GetComponent<TComponent>() where TComponent : Component;
        bool IsFromPool(Pool targetPool);

        void StartLifetimeTimer(float lifetime);
    }
}
