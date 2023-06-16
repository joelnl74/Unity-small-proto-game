using Singleton;
using UnityEngine;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private AudioSource m_vfx_audio_source;

    public void PlayOneShot(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        m_vfx_audio_source.PlayOneShot(clip);
    }
}
