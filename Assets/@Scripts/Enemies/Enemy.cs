using Data;
using UnityEngine;

namespace Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform headshotPoint;

        private EnemyData _data;
        private EnemyDisposer _disposer;

        public Vector3 Position => transform.position;
        public Vector3 HeadshotPosition => headshotPoint.position;
        public float Speed => _data.speed;
        public Vector3 Forward => Vector3.Normalize(Destination - Position);
        public Vector3 Destination { get; private set; }
        public bool IsAlive => gameObject.activeInHierarchy;

        public void Inject(EnemyData data, Vector3 target, EnemyDisposer disposer)
        {
            _data = data;
            Destination = target;
            _disposer = disposer;
        }

        public void CheckDestinationReached()
        {
            if (Vector3.Distance(transform.position, Destination) <= _data.reachDistance)
            {
                _disposer.OnEnemyReachedDestination(this);
            }
        }

        public void TakeDamage(float damage)
        {
            _data.currentHp -= damage;
            if (_data.currentHp <= 0)
            {
                _disposer.OnEnemyKilled(this);
            }
        }
    }
}