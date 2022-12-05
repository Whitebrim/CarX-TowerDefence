using Enemies;
using Services;
using UnityEngine;

namespace Tower
{
    public class CannonTower : Tower
    {
        private EnemySpawner _enemySpawner;
        private Enemy _target;

        private void Update()
        {
            _target ??= _enemySpawner.GetNearestEnemy(transform.position);
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