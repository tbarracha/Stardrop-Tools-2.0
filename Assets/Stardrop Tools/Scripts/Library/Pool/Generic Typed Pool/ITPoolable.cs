﻿
namespace StardropTools.Pool.Generic
{
    /// <summary>
    /// Interface used in all Components that you want to be part of a TPool (Generic Type Pool)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITPoolable<T> where T : UnityEngine.Component
    {
        public void SetPoolItem(TPoolItem<T> poolItem);

        public void OnSpawn();
        public void OnDespawn();

        public void Despawn();
    }
}