using NTC.Global.Pool;
using UnityEngine;

namespace Enemies.Movement
{
    [RequireComponent(typeof(Enemy), typeof(Rigidbody))]
    public class RigidbodyEnemyMovement : MonoBehaviour, IPoolItem
    {
        private Enemy _enemy;
        private Rigidbody _rigidbody;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _enemy ??= GetComponent<Enemy>();
            _rigidbody ??= GetComponent<Rigidbody>();

            SetVelocity((_enemy.Destination - transform.position).normalized * _enemy.Speed);
        }

        private void SetVelocity(Vector3 force)
        {
            _rigidbody.velocity = force;
        }

        public void OnSpawn()
        {
            Init();
        }

        public void OnDespawn()
        {
        }
    }
}