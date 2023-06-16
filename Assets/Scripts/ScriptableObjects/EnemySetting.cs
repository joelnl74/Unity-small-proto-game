using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Enemy settings", menuName = "Data/Enemy settings", order = 1)]
    public class EnemySetting : ScriptableObject
    {
        [field: SerializeField] public float move_speed { get; private set; } = 5;
        [field: SerializeField] public float rotation_speed { get; private set; } = 200;
        [field: SerializeField] public float life_time { get; private set; } = 5;
        [field: SerializeField] public float size { get; private set; } = 1;
        [field: SerializeField] public int score { get; private set; } = 100;
        [field: SerializeField] public AudioClip enemy_hit_sound { get; private set; }

    }
}
