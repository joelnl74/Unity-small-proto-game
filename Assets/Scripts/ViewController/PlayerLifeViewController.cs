using UnityEngine;
using Messaging.Interfaces;
using Messaging;

namespace Game
{
    /// <summary>
    /// Player life view controller.
    /// </summary>
    public class PlayerLifeViewController : BaseViewController<PlayerLifeView>
        ,IPlayerLifeViewController
        ,ISubscriber<PlayerHitMessage>
    {
        private void Start()
        {
            MessageBus.Get().Subscribe(this);
            GameManager.Instance.onGameStateChanged += OnGameStateChanged;
        }

        public void OnMessage(PlayerHitMessage message)
            => UpdateLives(message.lives);

        /// <summary>
        /// Handle game state change.
        /// </summary>
        /// <param name="gameState">game state</param>
        private void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Main)
            {
                m_view.Configure();
            }
        }

        private void UpdateLives(int livesLeft)
            => m_view.UpdateView(livesLeft);
    }
}
