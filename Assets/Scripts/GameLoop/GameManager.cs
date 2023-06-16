using Singleton;
using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Stage main loop
    /// </summary>
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        #region events

        public Action<int> onScoreUpdate;
        public Action<GameState> onGameStateChanged;

        #endregion

        #region SerializedFields

        [Header("Layout")]
        [SerializeField] private Transform m_stage_transform;
        [SerializeField] private Player m_prefab_player;
        [SerializeField] private EnemySpawner m_prefab_enemy_spawner;

        #endregion

        #region Private fields

        private GameState m_game_state;

        #endregion

        #region API
        public void SetGameState(GameState state)
        {
            m_game_state = state;

            switch (m_game_state)
            {
                case GameState.Starting:
                    break;
                case GameState.Main:
                    StartMainLooop();
                    break;
                case GameState.Ending:
                    FinishGame();
                    break;
            }

            onGameStateChanged?.Invoke(state);
        }

        #endregion

        #region loop

        private void Start()
            => SetGameState(GameState.Starting);

        /// <summary>
        /// stage loop
        /// </summary>
        private IEnumerator StageCoroutine()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //exit stage
                    FinishGame();

                    yield break;
                }
                yield return null;
            }
        }
        #endregion

        void SetupStage()
        {
            // Can be set in the game scene by default if we split the main menu and game scenes, but for prototype this is fine.
            Player player = Instantiate(m_prefab_player, m_stage_transform);
            player.transform.position = new Vector3(0, -4, 0);

            CreateSpawner(new Vector3(0, 5, 0));
            CreateSpawner(new Vector3(-4, 5, 0));
        }

        private void CreateSpawner(Vector3 spawnPosition)
        {
            EnemySpawner spawner = Instantiate(m_prefab_enemy_spawner, m_stage_transform);
            spawner.transform.position = spawnPosition;
        }

        private void StartMainLooop()
        {
            SetupStage();
            StartCoroutine(StageCoroutine());
        }

        private void CleanupStage()
        {
            // Delete all object in Stage.
            for (var n = 0; n < m_stage_transform.childCount; ++n)
            {
                Transform temp = m_stage_transform.GetChild(n);
                GameObject.Destroy(temp.gameObject);
            }

            SetGameState(GameState.Starting);
        }

        private void FinishGame()
            => CleanupStage();
    }
}
