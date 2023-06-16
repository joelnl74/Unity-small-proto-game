using Game.Settings;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Player Bullet
    /// </summary>
    public class PlayerBullet : BaseSpawnAble<PlayerBullet>
    {
        [Header("Settings")]
        [SerializeField] private BulletSettings m_bullet_settings;

        private float m_time_to_live = 0;
        private bool isAlive;

        public override void OnSpawn(in PlayerBullet bullet)
        {
            isAlive = true;
            m_time_to_live = m_bullet_settings.life_time;
            AudioManager.Instance.PlayOneShot(m_bullet_settings.audio_clip);
        }

        private void Update()
        {
            transform.position += new Vector3(0, 1, 0) * m_bullet_settings.move_speed * Time.deltaTime;

            m_time_to_live -= Time.deltaTime;
            if (m_time_to_live <= 0 && isAlive)
            {
                isAlive = false;
                Despawn(this);
            }
        }
    }
}
