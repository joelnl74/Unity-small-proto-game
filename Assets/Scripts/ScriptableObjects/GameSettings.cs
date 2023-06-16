using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Game setting", menuName = "Data/Game settings", order = 0)]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public int game_round_max_time { get; private set; } = 60;
    }
}
