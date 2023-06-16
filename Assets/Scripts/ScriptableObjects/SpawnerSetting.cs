using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Spawner setting", menuName = "Data/Spawner settings", order = 2)]
    public class SpawnerSetting : ScriptableObject
    {
        [field: SerializeField] public float spawn_radius { get; private set; } = 1;
        [field: SerializeField] public float min_interval { get; private set; } = 1;
        [field: SerializeField] public float max_interval { get; private set; } = 1;
        [field: SerializeField] public Enemy enemy_prefab { get; private set; }
        [field: SerializeField] public float increase_difficulty_time { get; private set; } = 1;
    }
}
