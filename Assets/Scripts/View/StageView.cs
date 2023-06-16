using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// Stage view logic.
    /// </summary>
    public class StageView : MonoBehaviour
        ,IStageView
    {
        [Header("Main")]
        [SerializeField] private Transform m_root_window;

        [Header("Text fields")]
        [SerializeField] private Text m_timer_text;

        public void ToggleView(bool active)
            => m_root_window.gameObject.SetActive(active);

        public void SetTimer(int value)
            => m_timer_text.text = $"Time left: {value}";
    }
}
