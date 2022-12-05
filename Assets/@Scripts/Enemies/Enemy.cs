using Data;
using Services;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        protected EnemyData Data;
        protected EnemySpawner Spawner;

        private Vector3 _target;

        public Vector3 Position => transform.position;
        public float Speed => Data.speed;
        public Vector3 Target => _target;

        public void Constructor(EnemyData data, Vector3 target, EnemySpawner spawner)
        {
            Data = data;
            _target = target;
            Spawner = spawner;
        }

        public void CheckDestinationReached()
        {
            if (Vector3.Distance(transform.position, Target) <= Data.reachDistance)
            {
                Spawner.OnEnemyReachedDestination(this);
            }
        }

        public void TakeDamage(float damage)
        {
            Data.currentHp -= damage;
            if (Data.currentHp < 0)
            {
                Spawner.OnEnemyKilled(this);
            }
        }
    }
}