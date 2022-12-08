using Core.Services.AssetManagement;
using Data;
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
        private readonly EnemySpawner _spawner;
        public readonly List<Enemy> EnemyList;

        public EnemyFactory(Vector3 spawnPosition, Vector3 targetPosition, EnemySpawner spawner)
        {
            _spawnPosition = spawnPosition;
            _targetPosition = targetPosition;
            _spawner = spawner;
            EnemyList = new List<Enemy>();
        }

        public Enemy Create(EnemyConfig enemy)
        {
            Enemy instance = NightPool.Spawn(AddressablesProvider.LoadPrefab<Enemy>(enemy.Prefab), _spawnPosition,
                Quaternion.identity);
            instance.Inject(enemy.Data, _targetPosition, _spawner.Disposer);
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