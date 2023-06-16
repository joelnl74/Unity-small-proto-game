using ObjectPooling;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Handles shared code for spawning objects in the scene.
    /// </summary>
    /// <typeparam name="T">Spawner class.</typeparam>
    public abstract class BaseSpawnerController<T> : MonoBehaviour,
    ISpawnerController<T> where T : BaseSpawnAble<T>
    {
        [SerializeField] private T m_prefab;

        private ObjectPool<T> m_pool;

        #region LifeCycle

        private void Awake()
            => m_pool = new ObjectPool<T>(OnCreate, OnSpawn, OnRelease, OnDestory, true, 10);

        private void OnDestroy()
            => m_pool.Clear();

        #endregion

        #region PublicAPI

        public T Get()
            => m_pool.Get();

        #endregion

        #region ISpawnerController

        /// <summary>
        /// Called when object need to be created.
        /// </summary>
        /// <returns></returns>
        protected virtual T OnCreate()
        {
            T newObject = Instantiate(m_prefab, transform.parent);
            newObject.transform.position = transform.position;

            newObject.onDespawn += OnDespawn;

            return newObject;
        }

        /// <summary>
        /// Called when object is spawned (get from pool)
        /// </summary>
        /// <param name="tObject"></param>
        protected virtual void OnSpawn(T tObject)
        {
            tObject.gameObject.SetActive(true);
            tObject.transform.position = transform.position;
            tObject.transform.rotation = Quaternion.identity;
            tObject.transform.localScale = Vector3.one;

            tObject.OnSpawn(tObject);
        }

        /// <summary>
        /// Called when object is being put back into the pool.
        /// </summary>
        /// <param name="tObject"></param>
        protected virtual void OnDespawn(T tObject)
        {
            tObject.gameObject.SetActive(false);

            if (tObject.isActiveAndEnabled)
            {
                return;
            }

            m_pool.Release(tObject);
        }

        /// <summary>
        /// Called when object is destroyed.
        /// </summary>
        /// <param name="tObject"></param>
        protected virtual void OnDestory(T tObject)
            => tObject.onDespawn -= OnDespawn;

        /// <summary>
        /// Called when object is released.
        /// </summary>
        /// <param name="tObject">Object we wish to release.</param>
        protected virtual void OnRelease(T tObject) { }

        #endregion
    }
}
