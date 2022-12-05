using Core.Infrastructure.Services;
using Core.Services;
using Data;
using Enemies;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class EnemySpawner : IService
    {
        private readonly LevelConfig _levelConfig;
        private readonly EnemyFactory _factory;

        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _spawnCoroutine;

        public EnemySpawner(ICoroutineRunner coroutineRunner, LevelConfig levelConfig,
            Vector3 spawnPosition, Vector3 targetPosition)
        {
            _levelConfig = levelConfig;
            _factory = new EnemyFactory(spawnPosition, targetPosition, this);

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

        private void SpawnEnemy(EnemyConfig enemy) => _factory.Create(enemy);

        public Enemy GetNearestEnemy(Vector3 position)
        {
            if (_factory.EnemyList.Count == 0) return null;

            var minDist = float.PositiveInfinity;
            var index = -1;
            for (var i = 0; i < _factory.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(_factory.EnemyList[i].Position, position);
                if (distance < minDist)
                {
                    minDist = distance;
                    index = i;
                }
            }
            return _factory.EnemyList[index];
        }

        public void OnEnemyKilled(Enemy enemy)
        {
            _factory.Destroy(enemy);
        }

        public void OnEnemyReachedDestination(Enemy enemy)
        {
            _factory.Destroy(enemy);
        }
    }
}