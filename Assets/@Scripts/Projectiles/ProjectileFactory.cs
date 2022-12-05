using Core.Services.AssetManagement;
using Data;
using NTC.Global.Pool;
using UnityEngine;

namespace Projectiles
{
    internal class ProjectileFactory
    {
        private readonly ProjectileSpawner _spawner;

        public ProjectileFactory(ProjectileSpawner spawner)
        {
            _spawner = spawner;
        }

        public Projectile Create(ProjectileConfig projectile, Vector3 position, Quaternion quaternion)
        {
            Projectile instance = NightPool.Spawn(AddressablesProvider.LoadPrefab<Projectile>(projectile.Prefab), position, Quaternion.identity);
            instance.Constructor(projectile.Data, _spawner);
            return instance;
        }

        public void Destroy(Projectile projectile)
        {
            NightPool.Despawn(projectile);
        }
    }
}