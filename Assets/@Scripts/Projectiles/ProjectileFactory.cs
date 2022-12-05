using System;
using System.Collections.Generic;
using Data;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Projectiles
{
    internal class ProjectileFactory
    {
        private readonly ProjectileSpawner _spawner;

        private readonly Dictionary<string, Projectile> _cache;
        public ProjectileFactory(ProjectileSpawner spawner)
        {
            _cache = new Dictionary<string, Projectile>();

            _spawner = spawner;
        }

        public Projectile Create(ProjectileConfig projectile, Vector3 position, Quaternion quaternion)
        {
            Projectile instance = NightPool.Spawn(LoadEnemyPrefab(projectile.Prefab), position, Quaternion.identity);
            instance.Constructor(projectile.Data, _spawner);
            return instance;
        }

        public void Destroy(Projectile projectile)
        {
            NightPool.Despawn(projectile);
        }

        private Projectile LoadEnemyPrefab(AssetReferenceGameObject assetReference)
        {
            if (!_cache.ContainsKey(assetReference.AssetGUID))
            {
                _cache.Add(assetReference.AssetGUID, assetReference.LoadAssetAsync().WaitForCompletion().GetComponent<Projectile>());
            }
            return _cache[assetReference.AssetGUID];
        }

        private void LoadEnemyPrefabAsync(AssetReferenceGameObject assetReference, Action<Projectile> onLoad)
        {
            if (!_cache.ContainsKey(assetReference.AssetGUID))
            {
                assetReference.LoadAssetAsync().Completed += (result) =>
                {
                    _cache.Add(assetReference.AssetGUID, result.Result.GetComponent<Projectile>());
                    onLoad?.Invoke(_cache[assetReference.AssetGUID]);
                };
                return;
            }
            onLoad?.Invoke(_cache[assetReference.AssetGUID]);
        }
    }
}