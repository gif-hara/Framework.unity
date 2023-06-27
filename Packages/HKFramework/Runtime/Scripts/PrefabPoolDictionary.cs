using System;
using System.Collections.Generic;
using UnityEngine;

namespace HK.Framework.PoolSystems
{
    /// <summary>
    /// <see cref="PrefabPool{T}"/>を辞書で管理するクラス
    /// </summary>
    public sealed class PrefabPoolDictionary<T> : IDisposable where T : Component
    {
        private readonly Dictionary<T, PrefabPool<T>> table = new();

        public PrefabPool<T> Get(T prefab)
        {
            if (!this.table.ContainsKey(prefab))
            {
                this.table.Add(prefab, new PrefabPool<T>(prefab));
            }

            return this.table[prefab];
        }

        public void ClearAll()
        {
            foreach (var pair in this.table)
            {
                pair.Value.Clear();
            }

            this.table.Clear();
        }

        public void Dispose()
        {
            foreach (var pair in this.table)
            {
                pair.Value.Dispose();
            }

            this.table.Clear();
        }
    }
}
