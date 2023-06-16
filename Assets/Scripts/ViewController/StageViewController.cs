using Game.Settings;
using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// State view controller.
    /// </summary>
    public class StageViewController : BaseViewController<StageView>
        , IStageViewController
    {
        [SerializeField] GameSettings m_game_settings;

        private GameManager m_stageLoop;
        private int looptimer;

        #region LifeCycle
        private void Start()
        {
            m_stageLoop = GameManager.Instance;
            m_stageLoop.onGameStateChanged += OnGameStateChanged;

            looptimer = m_game_settings.game_round_max_time;
        }

        private void OnDestroy()
        {
            if (m_stageLoop == null)
            {
                return;
            }
        }

        #endregion

        #region Private Methods
        private void Clear()
        {
            m_view.ToggleView(false);
            m_view.SetTimer(m_game_settings.game_round_max_time);

            StopAllCoroutines();
        }

        private IEnumerator MainCoroutine()
        {
            while (true)
            {
                if (looptimer <= 0)
                {
                    m_stageLoop.SetGameState(GameState.Ending);
                }

                yield return new WaitForSeconds(1);

                looptimer--;
                m_view.SetTimer(looptimer);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Handle events based on the game state.
        /// </summary>
        /// <param name="gameState">Current state the game is in.</param>
        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Starting:
                    m_view.ToggleView(false);
                    break;
                case GameState.Main:
                    looptimer = m_game_settings.game_round_max_time;
                    m_view.ToggleView(true);
                    StartCoroutine(MainCoroutine());
                    break;
                case GameState.Ending:
                    Clear();
                    break;
            }
        }

        #endregion
    }
}
