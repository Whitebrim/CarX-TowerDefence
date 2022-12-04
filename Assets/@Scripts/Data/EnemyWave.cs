using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct EnemyWave
    {
        [Tooltip("Тип противников в этой волне")]
        public EnemyData Enemy;

        [Tooltip("Количество противников в этой волне")]
        public int NumberOfEnemies;

        [Tooltip("Задержка перед началом этой волны в секундах")]
        public float WaveStartDelay;

        [Tooltip("Задержка между спавном противника в секундах")]
        public float EnemySpawnDelay;
    }
}