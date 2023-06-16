using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Bullet Setting", menuName = "Data/Bullet settings", order = 2)]
    public class BulletSettings : ScriptableObject
    {
        [field: SerializeField] public AudioClip audio_clip { get; private set; }
        [field: SerializeField] public float move_speed { get; private set; } = 5;
        [field: SerializeField] public float life_time { get; private set; } = 2;
    }
}
