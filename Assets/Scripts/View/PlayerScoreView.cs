using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// Player score view logic.
    /// </summary>
    public class PlayerScoreView : MonoBehaviour
        , IPlayerScoreView
    {
        [SerializeField] private Text m_stage_score_text;

        public void Configure()
            => DidLoadData(0);

        public void DidLoadData(int score)
            => m_stage_score_text.text = $"Score {score:00000}";
    }
}
