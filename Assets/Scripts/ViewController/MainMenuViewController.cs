using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Main menu view controller.
    /// </summary>
    public class MainMenuViewController : BaseViewController<MainMenuView>
    {
        private void Start()
            => GameManager.Instance.onGameStateChanged += OnGameStateChanged;

        /// <summary>
        /// Title loop.
        /// </summary>
        private IEnumerator TitleCoroutine()
        {
            //waiting game start
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.SetGameState(GameState.Main);

                    yield break;
                }
                yield return null;
            }
        }

        /// <summary>
        /// Handle game state change.
        /// </summary>
        /// <param name="gameState">game state</param>
        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Starting:
                    m_view.ToggleView(true);
                    StartCoroutine(TitleCoroutine());
                    break;
                case GameState.Main:
                    StopAllCoroutines();
                    m_view.ToggleView(false);
                    break;
            }
        }
    }
}
