using NTC.Global.Pool;
using UnityEngine;

namespace Projectiles.Movement
{
    [RequireComponent(typeof(Projectile), typeof(Rigidbody))]
    public class RigidbodyProjectileMovement : MonoBehaviour, IPoolItem
    {
        private Projectile _projectile;
        private Rigidbody _rigidbody;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _projectile ??= GetComponent<Projectile>();
            _rigidbody ??= GetComponent<Rigidbody>();

            SetVelocity(transform.forward * _projectile.Speed);
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