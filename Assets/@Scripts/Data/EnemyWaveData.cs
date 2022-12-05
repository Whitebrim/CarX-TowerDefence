using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct EnemyWaveData
    {
        [Tooltip("Тип противников в этой волне")]
        public EnemyConfig enemy;

        [Tooltip("Количество противников в этой волне")]
        public int numberOfEnemies;

        [Tooltip("Задержка перед началом этой волны в секундах")]
        public float waveStartDelay;

        [Tooltip("Задержка между спавном противника в секундах")]
        public float enemySpawnDelay;
    }
}