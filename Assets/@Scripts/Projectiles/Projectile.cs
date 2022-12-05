using System.Collections;
using Data;
using Enemies;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        private const float Lifetime = 5f;

        protected ProjectileData Data;
        protected ProjectileSpawner Spawner;

        private Coroutine _delayedDestroyCoroutine;

        public float Speed => Data.speed;
        public float Damage => Data.damage;

        public void Constructor(ProjectileData data, ProjectileSpawner spawner)
        {
            Data = data;
            Spawner = spawner;

            _delayedDestroyCoroutine = StartCoroutine(DelayedDestroy(Lifetime));
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(Damage);
            }

            KillProjectile();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(Damage);
            }

            KillProjectile();
        }

        public void KillProjectile()
        {
            if (_delayedDestroyCoroutine != null) StopCoroutine(_delayedDestroyCoroutine);
            Spawner.OnProjectileKilled(this);
        }

        private IEnumerator DelayedDestroy(float delay)
        {
            yield return new WaitForSeconds(delay);
            KillProjectile();
        }
    }
}
