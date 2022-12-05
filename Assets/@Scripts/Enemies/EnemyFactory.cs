using System;
using NTC.Global.Pool;
using Services;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Enemies
{
    internal class EnemyFactory
    {
        private readonly Vector3 _spawnPosition;
        private readonly Vector3 _targetPosition;
        private readonly EnemySpawner _spawner;
        public readonly List<Enemy> EnemyList;

        private readonly Dictionary<string, Enemy> _cache;

        public EnemyFactory(Vector3 spawnPosition, Vector3 targetPosition, EnemySpawner spawner)
        {
            _cache = new Dictionary<string, Enemy>();

            _spawnPosition = spawnPosition;
            _targetPosition = targetPosition;
            _spawner = spawner;
            EnemyList = new List<Enemy>();
        }

        public Enemy Create(EnemyConfig enemy)
        {
            Enemy instance = NightPool.Spawn(LoadEnemyPrefab(enemy.Prefab), _spawnPosition, Quaternion.identity);
            instance.Constructor(enemy.Data, _targetPosition, _spawner);
            EnemyList.Add(instance);
            return instance;
        }

        public void Destroy(Enemy enemy)
        {
            EnemyList.Remove(enemy);
            NightPool.Despawn(enemy);
        }

        private Enemy LoadEnemyPrefab(AssetReferenceGameObject assetReference)
        {
            if (!_cache.ContainsKey(assetReference.AssetGUID))
            {
                _cache.Add(assetReference.AssetGUID, assetReference.LoadAssetAsync().WaitForCompletion().GetComponent<Enemy>());
            }
            return _cache[assetReference.AssetGUID];
        }

        private void LoadEnemyPrefabAsync(AssetReferenceGameObject assetReference, Action<Enemy> onLoad)
        {
            if (!_cache.ContainsKey(assetReference.AssetGUID))
            {
                assetReference.LoadAssetAsync().Completed += (result) =>
                {
                    _cache.Add(assetReference.AssetGUID, result.Result.GetComponent<Enemy>());
                    onLoad?.Invoke(_cache[assetReference.AssetGUID]);
                };
                return;
            }
            onLoad?.Invoke(_cache[assetReference.AssetGUID]);
        }
    }
}