using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace HK.Framework.PoolSystems
{
    /// <summary>
    /// </summary>
    public sealed class PrefabPool<T> : IDisposable where T : Component
    {
        private readonly ObjectPool<T> objectPool;

        public PrefabPool(T original, int capacity = 16, int maxSize = 128)
        {
            this.objectPool = new ObjectPool<T>(
                createFunc: () => Object.Instantiate(original),
                actionOnGet: target => target.gameObject.SetActive(true),
                actionOnRelease: target => target.gameObject.SetActive(false),
                actionOnDestroy: target => Object.Destroy(target.gameObject),
                collectionCheck: true,
                defaultCapacity: capacity,
                maxSize: maxSize
                );
        }

        public T Get()
        {
            return this.objectPool.Get();
        }

        public void Release(T element)
        {
            this.objectPool.Release(element);
        }

        public void Clear()
        {
            this.objectPool.Clear();
        }

        public void Dispose()
        {
            this.objectPool?.Dispose();
        }
    }
}
