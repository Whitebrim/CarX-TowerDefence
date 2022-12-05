using Services;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        protected EnemyData Data;
        protected EnemySpawner EnemySpawner;

        private Vector3 _target;

        public Vector3 Position => transform.position;
        public float Speed => Data.speed;
        public Vector3 Target => _target;

        public void Constructor(EnemyData data, Vector3 target, EnemySpawner enemySpawner)
        {
            Data = data;
            _target = target;
            EnemySpawner = enemySpawner;
        }

        public void CheckDestinationReached()
        {
            if (Vector3.Distance(transform.position, Target) <= Data.reachDistance)
            {
                EnemySpawner.OnEnemyReachedDestination(this);
            }
        }

        public void TakeDamage(float damage)
        {
            Data.currentHp -= damage;
            if (Data.currentHp < 0)
            {
                EnemySpawner.OnEnemyKilled(this);
            }
        }
    }
}