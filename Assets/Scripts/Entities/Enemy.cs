using Game.Settings;
using Messaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    /// <summary>
    /// Enemy class.
    /// </summary>
    public class Enemy : BaseSpawnAble<Enemy>
    {
        [Header("Parameter")]
        [SerializeField] List<EnemySetting> m_settings;

        private float m_life_time;
        private EnemySetting m_current_setting;

        //------------------------------------------------------------------------------
        public override void OnSpawn(in Enemy tObject)
        {
            m_current_setting = m_settings[Random.Range(0, m_settings.Count - 1)];
            m_life_time = m_current_setting.life_time;

            transform.localScale *= m_current_setting.size;

            StartCoroutine(MainCoroutine());
        }

        //
        private IEnumerator MainCoroutine()
        {
            while (true)
            {
                //move
                transform.position += new Vector3(0, -1, 0) * m_current_setting.move_speed * Time.deltaTime;

                //animation
                transform.rotation *= Quaternion.AngleAxis(m_current_setting.rotation_speed * Time.deltaTime, new Vector3(1, 1, 0));

                //lifetime
                m_life_time -= Time.deltaTime;
                if (m_life_time <= 0)
                {
                    DeleteObject();
                    yield break;
                }

                yield return null;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlayerBullet player_bullet = collision.transform.GetComponent<PlayerBullet>();

            if (player_bullet == null)
            {
                return;
            }

            DestroyByPlayer(player_bullet);
        }

        private void DestroyByPlayer(PlayerBullet a_player_bullet)
        {
            //add score
            MessageBus.Get().Publish(new ScoreUpdatedMessage { score = m_current_setting.score });

            //delete bullet
            if (a_player_bullet)
            {
                a_player_bullet.Despawn(a_player_bullet);
            }

            AudioManager.Instance.PlayOneShot(m_current_setting.enemy_hit_sound);

            //delete self
            DeleteObject();
        }

        private void DeleteObject()
        {
            StopAllCoroutines();
            onDespawn?.Invoke(this);
        }
    }
}
