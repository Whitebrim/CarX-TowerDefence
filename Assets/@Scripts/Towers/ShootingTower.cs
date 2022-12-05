using Data;
using Enemies;
using Projectiles;
using Services;
using UnityEngine;

namespace Towers
{
    public abstract class ShootingTower : Tower
    {
        protected ProjectileConfig Projectile { get; private set; }
        [field: SerializeField] protected Transform ShootingPoint { get; private set; }
        protected Enemy Target { get; private set; }
        protected ProjectileSpawner ProjectileSpawner { get; private set; }
        protected float lastShootTime;

        public void Constructor(TowerData towerData, EnemySpawner enemySpawner, ProjectileConfig projectileConfig)
        {
            Constructor(towerData, enemySpawner);
            Projectile = projectileConfig;
            ProjectileSpawner = new ProjectileSpawner();
        }

        protected virtual void Update()
        {
            FindNearestEnemy();
        }

        protected virtual void FindNearestEnemy()
        {
            if (Target is null || !Target.gameObject.activeInHierarchy)
            {
                Target = EnemySpawner.GetNearestEnemy(transform.position);
            }

            if (Target is null)
            {
                return;
            }

            if (Vector3.Distance(transform.position, Target.Position) > Data.AggroRadius)
            {
                Target = null;
                return;
            }

            AimAtTarget();
            TryShoot();
        }

        protected abstract void AimAtTarget();

        protected virtual void TryShoot()
        {
            if (Time.time >= lastShootTime + Data.ShootInterval)
            {
                lastShootTime = Time.time;
                Shoot();
            }
        }

        protected virtual Projectile Shoot()
        {
            return ProjectileSpawner.SpawnProjectile(Projectile, ShootingPoint.position, ShootingPoint.rotation);
        }
    }
}