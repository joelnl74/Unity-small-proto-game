using Messaging;
using Messaging.Interfaces;

namespace Game
{
    /// <summary>
    /// Player score view controller.
    /// </summary>
    public class PlayerScoreViewController : BaseViewController<PlayerScoreView>
        ,ISubscriber<ScoreUpdatedMessage>
    {
        protected int m_score;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            GameManager.Instance.onGameStateChanged += OnGameStateChanged;
            MessageBus.Get().Subscribe(this);
        }

        public void OnMessage(ScoreUpdatedMessage message)
        {
            m_score += message.score;
            m_view.DidLoadData(m_score);
        }

        /// <summary>
        /// Handle game state changed.
        /// </summary>
        /// <param name="gameState">game state</param>
        protected virtual void OnGameStateChanged(GameState gameState)
        {
            if (gameState == GameState.Main)
            {
                m_score = 0;
                m_view.Configure();
            }
        }
    }
}
