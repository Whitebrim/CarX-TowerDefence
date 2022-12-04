using Services;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float _hp;
        protected float _currentHp;
        [SerializeField] protected float _speed;
        protected float _reachDistance = 0.3f;

        protected EnemySpawner EnemySpawner;
        protected Vector3 _target;

        public Vector3 Position => transform.position;

        public void Constructor(Enemy reference, Vector3 target, EnemySpawner enemySpawner)
        {
            _hp = reference._hp;
            _currentHp = reference._hp;
            _speed = reference._speed;
            _reachDistance = reference._reachDistance;

            _target = target;
            EnemySpawner = enemySpawner;
        }

        protected void Update()
        {
            ReachDestination();
        }

        public void TakeDamage(float damage)
        {
            _currentHp -= damage;
            if (_currentHp < 0)
            {
                EnemySpawner.OnEnemyKilled(this);
            }
        }

        private void ReachDestination()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _target) <= _reachDistance)
            {
                EnemySpawner.OnEnemyReachedDestination(this);
            }
        }
    }
}