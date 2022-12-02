using System;
using Enemies;
using Services;
using UnityEngine;

namespace Tower
{
    public class CannonTower : MonoBehaviour, ITower
    {
        private Enemy _target;
        private EnemyController _enemyController;

        private void Update()
        {
            _target ??= _enemyController.GetNearestEnemy(transform.position);
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