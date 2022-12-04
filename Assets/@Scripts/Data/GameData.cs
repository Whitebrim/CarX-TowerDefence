using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Game Data", menuName = "Scriptable Object/Game Data", order = 0)]
    public class GameData : ScriptableObject
    {
        [Tooltip("Список всех уровней в игре")]
        public List<LevelData> Levels;
    }
}