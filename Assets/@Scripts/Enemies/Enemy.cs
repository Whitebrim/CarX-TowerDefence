using Services;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        protected EnemyData Data;

        protected EnemySpawner EnemySpawner;
        protected Vector3 Target;

        public Vector3 Position => transform.position;

        public void Constructor(EnemyData data, Vector3 target, EnemySpawner enemySpawner)
        {
            Data = data;
            Target = target;
            EnemySpawner = enemySpawner;
        }

        protected void Update()
        {
            ReachDestination();
        }

        public void TakeDamage(float damage)
        {
            Data.currentHp -= damage;
            if (Data.currentHp < 0)
            {
                EnemySpawner.OnEnemyKilled(this);
            }
        }

        private void ReachDestination()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Data.speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, Target) <= Data.reachDistance)
            {
                EnemySpawner.OnEnemyReachedDestination(this);
            }
        }
    }
}