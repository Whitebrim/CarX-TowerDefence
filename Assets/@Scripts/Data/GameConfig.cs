using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Scriptable Object/Game Data", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Tooltip("Список всех уровней в игре")]
        public List<LevelConfig> levels;
    }
}