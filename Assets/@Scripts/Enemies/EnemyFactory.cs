using NTC.Global.Pool;
using Services;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyFactory
    {
        private readonly Vector3 _spawnPosition;
        private readonly Vector3 _targetPosition;
        private readonly EnemyController _enemyController;
        public readonly List<Enemy> EnemyList;

        public EnemyFactory(Vector3 spawnPosition, Vector3 targetPosition, EnemyController enemyController)
        {
            _spawnPosition = spawnPosition;
            _targetPosition = targetPosition;
            _enemyController = enemyController;
            EnemyList = new List<Enemy>();
        }

        public Enemy Create(Enemy enemy)
        {
            var instance = NightPool.Spawn(enemy, _spawnPosition, Quaternion.identity);
            instance.Constructor(enemy, _targetPosition, _enemyController);
            EnemyList.Add(instance);
            return instance;
        }

        public void Destroy(Enemy enemy)
        {
            EnemyList.Remove(enemy);
            NightPool.Despawn(enemy);
        }
    }
}