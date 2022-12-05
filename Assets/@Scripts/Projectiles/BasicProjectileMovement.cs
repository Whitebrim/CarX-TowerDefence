using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class BasicProjectileMovement : MonoBehaviour
    {
        private Projectile _projectile;

        private void Start()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += transform.forward * (_projectile.Speed * Time.deltaTime);
        }
    }
}