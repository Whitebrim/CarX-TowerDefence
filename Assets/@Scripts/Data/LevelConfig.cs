using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Level 0", menuName = "Scriptable Object/Level Config", order = 50)]
    public class LevelConfig : ScriptableObject
    {
        //public GameObject mapPrefab;
        public List<EnemyWaveData> enemyWaves;
    }
}