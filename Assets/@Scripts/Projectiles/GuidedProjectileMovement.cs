using Enemies;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Projectile))]
    public class GuidedProjectileMovement : MonoBehaviour
    {
        private Projectile _projectile;
        public Enemy Target;

        private void Start()
        {
            _projectile = GetComponent<Projectile>();
        }

        private void Update()
        {
            if (!Target.IsAlive)
            {
                _projectile.KillProjectile();
                return;
            }
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.HeadshotPosition, _projectile.Speed * Time.deltaTime);
        }
    }
}