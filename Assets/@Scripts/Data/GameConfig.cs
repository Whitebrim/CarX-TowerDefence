using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Scriptable Object/Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Tooltip("Список всех уровней в игре")]
        public List<LevelConfig> levels;
    }
}