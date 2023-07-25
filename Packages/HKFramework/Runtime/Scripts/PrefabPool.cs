using System;
using System.Collections.Generic;
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
        
        public readonly List<T> ActiveElements = new();

        public readonly HashSet<T> All = new();

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
            var element = this.objectPool.Get();
            this.ActiveElements.Add(element);
            if (!this.All.Contains(element))
            {
                this.All.Add(element);
            }
            return element;
        }

        public void Release(T element)
        {
            this.objectPool.Release(element);
            this.ActiveElements.Remove(element);
        }
        
        public void ReleaseAll()
        {
            foreach (var element in this.ActiveElements)
            {
                this.objectPool.Release(element);
            }
            
            this.ActiveElements.Clear();
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
