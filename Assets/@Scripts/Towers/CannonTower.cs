using Projectiles;
using UnityEngine;

namespace Towers
{
    public class CannonTower : ShootingTower
    {
        [SerializeField] private Transform PivotPoint;
        [SerializeField] private Transform Pitch;
        [SerializeField] private Transform Yaw;

        protected override void AimAtTarget()
        {
            Vector3 lookDirection = Target.Position + Vector3.up - PivotPoint.position;
            Debug.DrawLine(PivotPoint.position, Target.Position, Color.cyan);
            Yaw.localRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));
            Pitch.localRotation = Quaternion.LookRotation(new Vector3(0, lookDirection.y, lookDirection.z));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PivotPoint.position, Data.AggroRadius);
        }
    }
}