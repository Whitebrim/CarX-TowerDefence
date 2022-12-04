using Core.Infrastructure.Services;
using Core.Services;
using Core.Services.AssetManagement;
using Data;
using Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class EnemySpawner : IService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LevelData _levelData;
        private readonly EnemyFactory _enemyFactory;

        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _spawnCoroutine;

        private readonly Dictionary<string, Enemy> _prefabCache;

        public EnemySpawner(IAssetProvider assetProvider, ICoroutineRunner coroutineRunner, LevelData levelData,
            Vector3 spawnPosition, Vector3 targetPosition)
        {
            _prefabCache = new Dictionary<string, Enemy>();

            _assetProvider = assetProvider;
            _levelData = levelData;
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
            if (_levelData.EnemyWaves.Count > 0)
            {
                EnemyWave wave = _levelData.EnemyWaves.First();
                _levelData.EnemyWaves.RemoveAt(0);
                StartWave(wave);
            }
            else
            {
                // TODO all enemies are spawned, wait for remaining and finish the level
            }
        }

        private void StartWave(EnemyWave wave)
        {
            _spawnCoroutine = _coroutineRunner.StartCoroutine(SpawnWaveCoroutine(wave, StartNextWave));
        }

        private void StopWave()
        {
            _coroutineRunner.StopCoroutine(_spawnCoroutine);
        }

        private IEnumerator SpawnWaveCoroutine(EnemyWave wave, Action onWaveSpawned)
        {
            yield return new WaitForSeconds(wave.WaveStartDelay);
            while (wave.NumberOfEnemies > 0)
            {
                SpawnEnemy(GetEnemyPrefab(wave.Enemy.PrefabPath));
                wave.NumberOfEnemies--;
                yield return new WaitForSeconds(wave.EnemySpawnDelay);
            }
            onWaveSpawned?.Invoke();
        }

        private Enemy GetEnemyPrefab(string path)
        {
            if (!_prefabCache.ContainsKey(path))
            {
                _prefabCache.Add(path, _assetProvider.Load<GameObject>(path).GetComponent<Enemy>());
            }

            return _prefabCache[path];
        }

        private void SpawnEnemy(Enemy enemy) => _enemyFactory.Create(enemy);

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