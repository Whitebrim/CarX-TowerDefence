using Data;
using Enemies;
using Services;
using UnityEngine;

namespace Towers
{
    public abstract class ShootingTower : Tower
    {
        protected ProjectileConfig Projectile { get; private set; }
        [field: SerializeField] protected Transform ShootingPoint { get; private set; }

        public void Constructor(TowerData towerData, EnemySpawner enemySpawner, ProjectileConfig projectileConfig)
        {
            Constructor(towerData, enemySpawner);
            Projectile = projectileConfig;
        }

        private Enemy _target;

        private void Update()
        {
            _target ??= EnemySpawner.GetNearestEnemy(transform.position);
            if (_target is not null)
            {
                AimAtTarget();
            }
        }

        private void AimAtTarget()
        {
            Debug.Log("Aiming");
        }
    }
}