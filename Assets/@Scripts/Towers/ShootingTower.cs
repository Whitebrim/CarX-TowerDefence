using Data;
using Enemies;
using Projectiles;
using UnityEngine;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Towers
{
    public abstract class ShootingTower : Tower
    {
        [field: SerializeField] protected Transform ShootingPoint { get; private set; }
        protected ProjectileConfig Projectile { get; private set; }
        protected Enemy Target { get; private set; }

        private ProjectileSpawner _projectileSpawner;
        private float _lastShootTime;

        public void Inject(TowerData towerData, EnemyLocator enemyLocator, ProjectileConfig projectileConfig)
        {
            Inject(towerData, enemyLocator);
            Projectile = projectileConfig;
            _projectileSpawner = new ProjectileSpawner();
        }

        protected virtual void Update()
        {
            FindNearestEnemy();
            if (Target is null) return;

            AimAtTarget();
            TryShoot();
        }

        protected virtual void FindNearestEnemy()
        {
            if (Target is null || !Target.IsAlive)
            {
                Target = EnemyLocator.GetNearestEnemy(transform.position);
            }

            if (Target is null)
            {
                return;
            }

            if (Vector3.Distance(transform.position, Target.HeadshotPosition) > Data.AggroRadius)
            {
                Target = null;
            }
        }

        protected abstract void AimAtTarget();

        private void TryShoot()
        {
            if (Time.time >= _lastShootTime + Data.ShootInterval)
            {
                _lastShootTime = Time.time;
                Shoot();
            }
        }

        protected virtual Projectile Shoot()
        {
            return _projectileSpawner.SpawnProjectile(Projectile, ShootingPoint.position, ShootingPoint.rotation);
        }
    }
}