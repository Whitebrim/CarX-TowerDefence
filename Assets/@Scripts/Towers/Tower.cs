using Data;
using Enemies;
using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Towers
{
    public abstract class Tower : MonoBehaviour
    {
        protected TowerData Data { get; private set; }
        protected EnemyLocator EnemyLocator { get; private set; }

        public void Inject(TowerData towerData, EnemyLocator enemyLocator)
        {
            EnemyLocator = enemyLocator;
            Data = towerData;
        }
    }
}