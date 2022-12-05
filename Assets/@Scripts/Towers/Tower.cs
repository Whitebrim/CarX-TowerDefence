using Data;
using Services;
using UnityEngine;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        protected TowerData Data { get; private set; }
        protected EnemySpawner EnemySpawner;

        public void Constructor(TowerData towerData, EnemySpawner enemySpawner)
        {
            EnemySpawner = enemySpawner;
            Data = towerData;
        }
    }
}