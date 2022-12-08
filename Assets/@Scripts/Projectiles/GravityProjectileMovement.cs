using NTC.Global.Pool;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class GravityProjectileMovement : MonoBehaviour, IPoolItem
    {
        private Projectile _projectile;
        private Vector3 _gravity;
        private Vector3 _velocity;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            Move();
        }

        public void OnSpawn()
        {
            Init();
        }

        public void OnDespawn() { }

        private void Init()
        {
            _projectile = GetComponent<Projectile>();
            _gravity = Physics.gravity;
            _velocity = transform.forward * _projectile.Speed;
        }

        private void Move()
        {
            _velocity += _gravity * Time.deltaTime;
            transform.position += _velocity * Time.deltaTime;
        }
    }
}