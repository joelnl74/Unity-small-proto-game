using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Player setting", menuName = "Data/Player settings", order = 4)]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float move_speed { get; private set; } = 4;

        [field: SerializeField] public float max_rotation { get; private set; } = 35;
        [field: SerializeField] public float min_rotation { get; private set; } = -35;
        [field: SerializeField] public float rotation_speed { get; private set; } = 70;

        [field: SerializeField] public float dodge_speed { get; private set; } = 6;
        [field: SerializeField] public float dodge_duration { get; private set; } = 0.5f;

        [field: SerializeField] public float fire_cooldown { get; private set; } = 0.2f;

    }
}
