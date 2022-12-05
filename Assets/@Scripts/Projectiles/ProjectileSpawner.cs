using Data;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileSpawner
    {
        private ProjectileFactory _factory;

        public ProjectileSpawner()
        {
            _factory = new ProjectileFactory(this);
        }

        public Projectile SpawnProjectile(ProjectileConfig projectile, Vector3 position, Quaternion rotation) =>
            _factory.Create(projectile, position, rotation);

        public void OnProjectileKilled(Projectile projectile)
        {
            _factory.Destroy(projectile);
        }
    }
}