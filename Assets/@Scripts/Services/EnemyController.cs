using System.Collections;
using Core.Infrastructure.Services;
using Core.Services;
using Core.Services.AssetManagement;
using Enemies;
using UnityEngine;

namespace Services
{
    public class EnemyController : IService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly float _spawnDelay;
        private int _spawnCount;
        private EnemyFactory _enemyFactory;
        private Coroutine _spawnCoroutine;

        private Enemy _enemyPrefab;

        public EnemyController(IAssetProvider assetProvider, Vector3 spawnPosition, Vector3 targetPosition, float spawnDelay)
        {
            _assetProvider = assetProvider;
            _spawnDelay = spawnDelay;
            _spawnCount = 0;
            LoadPrefabs();
            _enemyFactory = new EnemyFactory(spawnPosition, targetPosition, this);

            _spawnCoroutine = DI.Container.Single<ICoroutineRunner>().StartCoroutine(SpawnLoop());
        }

        private void LoadPrefabs()
        {
            _enemyPrefab = _assetProvider.Load<GameObject>("Basic Enemy").GetComponent<Enemy>();
        }

        public void SpawnEnemy(Enemy enemy) => _enemyFactory.Create(enemy);

        IEnumerator SpawnLoop()
        {
            while (true)
            {
                SpawnEnemy(_enemyPrefab);
                _spawnCount++;
                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        public Enemy GetNearestEnemy(Vector3 position)
        {
            var minDist = float.PositiveInfinity;
            var index = 0;
            for (var i = 0; i < _enemyFactory.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(_enemyFactory.EnemyList[i].Position, position);
                if (distance < minDist)
                {
                    minDist = distance;
                    index = i;
                }
            }
            return _enemyFactory.EnemyList[index];
        }

        public void OnEnemyKilled(Enemy enemy)
        {
            _enemyFactory.Destroy(enemy);
        }

        public void OnEnemyReachedDestination(Enemy enemy)
        {
            _enemyFactory.Destroy(enemy);
        }
    }
}
