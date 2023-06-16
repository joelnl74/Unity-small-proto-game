using ObjectPooling.Interfaces;
using System;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Base object spawn class.
    /// </summary>
    /// <typeparam name="T">Base object we wish to spawn.</typeparam>
    public abstract class BaseSpawnAble<T> : MonoBehaviour
        , ISpawnAble<T>
    {
        public Action<T> onDespawn;

        public abstract void OnSpawn(in T tObject);

        public void Despawn(in T tObject)
            => onDespawn?.Invoke(tObject);
    }
}
