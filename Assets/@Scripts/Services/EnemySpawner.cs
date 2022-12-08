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
    public class EnemySpawner : IService, IDisposable
    {
        public readonly EnemyDisposer Disposer;
        public readonly EnemyLocator Locator;

        private readonly LevelConfig _levelConfig;
        private readonly EnemyFactory _factory;

        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _spawnCoroutine;

        public EnemySpawner(ICoroutineRunner coroutineRunner, LevelConfig levelConfig,
            Vector3 spawnPosition, Vector3 targetPosition)
        {
            _levelConfig = levelConfig;
            _factory = new EnemyFactory(spawnPosition, targetPosition, this);
            Disposer = new EnemyDisposer(_factory);
            Locator = new EnemyLocator(_factory);

            _coroutineRunner = coroutineRunner;
        }

        ~EnemySpawner()
        {
            Dispose();
        }

        public void Dispose()
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

        private Enemy SpawnEnemy(EnemyConfig enemy) => _factory.Create(enemy);
    }
}