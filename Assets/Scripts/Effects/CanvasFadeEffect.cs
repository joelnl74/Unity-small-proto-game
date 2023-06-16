using System.Collections;
using UnityEngine;

/// <summary>
/// Canvas fade effect.
/// </summary>
public class CanvasFadeEffect : MonoBehaviour,
    IEffect
{
    [SerializeField] private CanvasGroup m_canvas_group;

    [SerializeField] private float m_fade_amount;
    [SerializeField] private float m_fade_time;

    public void Play()
        => StartCoroutine(DoFade());

    public void Stop()
        => StopAllCoroutines();

    public void Reset()
        => m_canvas_group.alpha = 1;

    private IEnumerator DoFade()
    {
        float t = 0.0f;
        float fadeValue = m_canvas_group.alpha < 1 ? 1 : m_fade_amount;

        while (t < m_fade_time)
        {
            m_canvas_group.alpha = Mathf.MoveTowards(m_canvas_group.alpha, fadeValue, m_fade_time);

            yield return null;
        }
    }
}