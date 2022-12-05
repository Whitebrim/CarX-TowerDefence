using Core.Infrastructure.Services;
using Core.Services;
using Core.Services.AssetManagement;
using Data;
using Enemies;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class EnemySpawner : IService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LevelConfig _levelConfig;
        private readonly EnemyFactory _enemyFactory;

        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _spawnCoroutine;

        public EnemySpawner(IAssetProvider assetProvider, ICoroutineRunner coroutineRunner, LevelConfig levelConfig,
            Vector3 spawnPosition, Vector3 targetPosition)
        {
            _assetProvider = assetProvider;
            _levelConfig = levelConfig;
            _enemyFactory = new EnemyFactory(spawnPosition, targetPosition, this);

            _coroutineRunner = coroutineRunner;
        }

        ~EnemySpawner()
        {
            StopWave();
        }

        public void StartLevel()
        {
            StartNextWave();
        }

        private void StartNextWave()
        {
            if (_levelConfig.enemyWaves.Count > 0)
            {
                EnemyWaveData waveData = _levelConfig.enemyWaves.First();
                _levelConfig.enemyWaves.RemoveAt(0);
                StartWave(waveData);
            }
            else
            {
                // TODO all enemies are spawned, wait for remaining and finish the level
            }
        }

        private void StartWave(EnemyWaveData waveData)
        {
            _spawnCoroutine = _coroutineRunner.StartCoroutine(SpawnWaveCoroutine(waveData, StartNextWave));
        }

        private void StopWave()
        {
            _coroutineRunner.StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator SpawnWaveCoroutine(EnemyWaveData waveData, Action onWaveSpawned)
        {
            yield return new WaitForSeconds(waveData.waveStartDelay);
            while (waveData.numberOfEnemies > 0)
            {
                SpawnEnemy(waveData.enemy);
                waveData.numberOfEnemies--;
                yield return new WaitForSeconds(waveData.enemySpawnDelay);
            }
            onWaveSpawned?.Invoke();
        }

        private void SpawnEnemy(EnemyConfig enemy) => _enemyFactory.Create(enemy);

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