using Data;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        protected ProjectileData Data;
        protected ProjectileSpawner Spawner;

        public float Speed => Data.speed;
        public float Damage => Data.damage;

        public void Constructor(ProjectileData data, ProjectileSpawner spawner)
        {
            Data = data;
            Spawner = spawner;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(Damage);
            }

            KillProjectile();
        }

        public void KillProjectile()
        {
            Spawner.OnProjectileKilled(this);
        }
    }
}
