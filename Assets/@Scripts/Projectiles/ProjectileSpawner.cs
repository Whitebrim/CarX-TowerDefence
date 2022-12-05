using Core.Services.AssetManagement;
using Data;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileSpawner
    {
        private readonly IAssetProvider _assetProvider;
        private ProjectileFactory _factory;

        public ProjectileSpawner(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _factory = new ProjectileFactory(this);
        }

        public void SpawnProjectile(ProjectileConfig projectile, Vector3 position, Quaternion rotation) =>
            _factory.Create(projectile, position, rotation);

        public void OnProjectileKilled(Projectile projectile)
        {
            _factory.Destroy(projectile);
        }
    }
}