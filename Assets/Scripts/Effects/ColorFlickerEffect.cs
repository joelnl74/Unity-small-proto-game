using System.Collections;
using UnityEngine;

/// <summary>
/// Make the renderer flicker color.
/// </summary>
public class ColorFlickerEffect : MonoBehaviour,
    IEffect
{
    [Header("Components")]
    [SerializeField] private MeshRenderer m_renderer;
    [SerializeField] private Color m_flicker_color;

    [Header("Parameters")]
    [SerializeField] private int m_times;
    [SerializeField] private float m_interval;

    private Color originalColor;

    private void Start()
        => originalColor = m_renderer.material.color;

    public void Play()
        => StartCoroutine(MainCoroutine());

    public void Stop()
        => StopAllCoroutines();

    private IEnumerator MainCoroutine()
    {
        bool swapToMainColor = false;

        for (int i = 0; i < m_times; i++)
        {
            m_renderer.material.color = swapToMainColor ? m_flicker_color : originalColor;

            yield return new WaitForSeconds(m_interval);

            swapToMainColor = !swapToMainColor;
        }

        m_renderer.material.color = originalColor;
    }
}