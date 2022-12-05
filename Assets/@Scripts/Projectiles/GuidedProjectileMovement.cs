using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class GuidedProjectileMovement : MonoBehaviour
    {
        private Projectile _projectile;
        private Transform _target;

        private void Start()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void Update()
        {
            if (!_target)
            {
                _projectile.KillProjectile();
                return;
            }
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _projectile.Speed * Time.deltaTime);
        }
    }
}