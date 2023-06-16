using Messaging;
using UnityEngine;

namespace Game
{

    /// <summary>
    /// Handles players life.
    /// </summary>
    public class PlayerLifeController : MonoBehaviour
        ,IPlayerLifeController
    {
        [Header("Effects")]
        [SerializeField] private ColorFlickerEffect m_colorFlickerEffect;

        [Header("Parameters")]
        [SerializeField] private int m_player_max_lives = 3;

        private int m_current_lives;

        private void Start()
            => Configure();

        /// <summary>
        /// Set players life back to default state.
        /// </summary>
        public void Configure()
        {
            m_current_lives = m_player_max_lives;
            MessageBus.Get().Publish(new PlayerHitMessage { lives = m_player_max_lives });
        }

        /// <summary>
        /// When hit decrease player life.
        /// </summary>
        /// <param name="collision">other collision hit</param>
        private void OnTriggerEnter(Collider collision)
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();

            if (enemy == null)
            {
                return;
            }

            m_current_lives--;
            m_colorFlickerEffect.Play();

            MessageBus.Get().Publish(new PlayerHitMessage { lives = m_current_lives });

            if (m_current_lives <= 0)
            {
                GameManager.Instance.SetGameState(GameState.Ending);
            }
        }

        private void OnDestroy()
            => m_colorFlickerEffect.Stop();   
    }
}
