using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Player life view logic.
    /// </summary>
    public class PlayerLifeView : MonoBehaviour
    , IPlayerLifeView
    {
        [SerializeField] private List<CanvasFadeEffect> m_fade_effects;

        public void Configure()
        {
            for (int i = 0; i < m_fade_effects.Count; i++)
            {
                m_fade_effects[i].Reset();
            }
        }

        public void UpdateView(int livesLeft)
            => m_fade_effects[Mathf.Clamp(livesLeft, 0, m_fade_effects.Count - 1)].Play();
    }
}
