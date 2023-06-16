using Game.Settings;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game
{
    /// <summary>
    /// Enemy SpawnPoint
    /// </summary>
    public class EnemySpawner : BaseSpawnerController<Enemy>
    {
        [Header("Parameter")]
        [SerializeField] List<SpawnerSetting> m_settings;

        private SpawnerSetting m_spawner_setting;

        private float m_timer;
        private int m_current_difficulty_index = 0;

        #region LifeCycle

        private void Start()
        {
            m_spawner_setting = m_settings.First();
            StartCoroutine(MainCoroutine());
        }

        private IEnumerator MainCoroutine()
        {
            while (true)
            {
                Get();

                float interval = Random.Range(m_spawner_setting.min_interval, m_spawner_setting.max_interval);

                yield return new WaitForSeconds(interval);

                CheckDifficulty(interval);
            }
        }

        #endregion

        #region SpawnAble
        protected override void OnSpawn(Enemy enemy)
        {
            base.OnSpawn(enemy);

            Vector3 position = enemy.transform.position;
            enemy.transform.position = new Vector3(position.x + Random.Range(-m_spawner_setting.spawn_radius, m_spawner_setting.spawn_radius), position.y, position.z);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Update difficulty setting for enemies.
        /// </summary>
        /// <param name="interval"></param>
        private void CheckDifficulty(float interval)
        {
            m_timer += interval;

            if (m_timer <= m_spawner_setting.increase_difficulty_time)
            {
                return;
            }

            m_timer = 0;

            m_current_difficulty_index++;
            m_spawner_setting = m_settings[Mathf.Clamp(m_current_difficulty_index, 0, m_settings.Count - 1)];
        }

        #endregion
    }
}
