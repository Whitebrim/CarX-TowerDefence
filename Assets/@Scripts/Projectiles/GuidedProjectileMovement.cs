using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class GuidedProjectileMovement : MonoBehaviour
    {
        private Projectile _projectile;
        public Transform Target;

        private void Start()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void Update()
        {
            if (!Target)
            {
                _projectile.KillProjectile();
                return;
            }
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, _projectile.Speed * Time.deltaTime);
        }
    }
}