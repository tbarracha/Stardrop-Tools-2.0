
using UnityEngine;
using StardropTools.Pool;

namespace StardropTools.Test
{
    //[System.Serializable]
    public class Cube : MonoBehaviour, IPoolable<Cube>
    {
        public PoolItem<Cube> originPool;
        public new GameObject gameObject;
        public new Transform transform;

        public void Despawn() => originPool.Despawn();

        public void OnDespawn()
        {
            //Debug.Log(name + " Despawned!");
        }

        public void OnSpawn()
        {
            //Debug.Log(name + " Spawned!");
        }

        public void SetPoolItem(PoolItem<Cube> pool) => originPool = pool;
    }
}