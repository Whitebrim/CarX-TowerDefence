using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level 0", menuName = "Scriptable Object/Level Data", order = 50)]
    public class LevelData : ScriptableObject
    {
        public List<EnemyWave> EnemyWaves;
    }
}