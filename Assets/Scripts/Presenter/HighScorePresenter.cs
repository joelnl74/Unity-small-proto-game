using UnityEngine;

namespace Game
{
    public class HighScorePresenter : BasePresenter<PlayerScoreView>,
        IHighScorePresenter
    {
        private const string m_high_score_key = "high_score_key";

        /// <summary>
        /// Load data and update view.
        /// </summary>
        public void LoadData()
        {
            int score = PlayerPrefs.GetInt(m_high_score_key);

            m_view.DidLoadData(score);
        }

        /// <summary>
        /// Save data.
        /// </summary>
        /// <param name="score"></param>
        public void SaveScore(int score)
        {
            int highScore = PlayerPrefs.GetInt(m_high_score_key);
            int newScore = score > highScore ? score : highScore;

            PlayerPrefs.SetInt(m_high_score_key, newScore);
        }
    }
}
