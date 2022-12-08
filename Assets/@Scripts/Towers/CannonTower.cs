using Projectiles;
using UnityEngine;

namespace Towers
{
    public class CannonTower : ShootingTower
    {
        [SerializeField] private Transform pivotPoint;
        [SerializeField] private Transform pitch;
        [SerializeField] private Transform yaw;

        protected override void AimAtTarget()
        {
            Vector3 aimAt = PredictInterception();
            Vector3 lookDirection = aimAt - pivotPoint.position;

            DebugDrawLine(aimAt);

            RotateTower(lookDirection);

            Debug.LogError(aimAt);
        }

        protected override Projectile Shoot()
        {
            Debug.LogError("Shooting");
            return base.Shoot();
        }

        private void RotateTower(Vector3 lookDirection)
        {
            yaw.localRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));
            pitch.localRotation = Quaternion.LookRotation(new Vector3(0, lookDirection.y, lookDirection.z));
        }

        /// <summary>
        /// <see href="https://www.reddit.com/r/gamedev/comments/16ceki/comment/c7vbu2j"/>
        /// </summary>
        /// <returns></returns>
        private Vector3 PredictInterception()
        {
            Vector3 v = Target.Forward * Target.Speed;
            Vector3 w = Target.HeadshotPosition - pivotPoint.position;
            float a = Vector3.Dot(v, v) - Mathf.Pow(Projectile.Data.speed, 2);
            float b = 2 * Vector3.Dot(w, v);
            float c = Vector3.Dot(w, w);
            float[] root = SolveQuadratic(a, b, c);
            float x = float.PositiveInfinity;
            foreach (float r in root)
            {
                if (r > 0)
                    x = Mathf.Min(r, x);
            }

            return Target.HeadshotPosition + v * x;
        }

        private float[] SolveQuadratic(float a, float b, float c)
        {
            float x1, x2;

            if (a == 0)
            {
                x1 = -c / b;
                return new[] { x1 };
            }

            float disc = (b * b) - (4 * a * c);
            float deno = 2 * a;
            switch (disc)
            {
                case > 0:
                    x1 = (-b / deno) + (Mathf.Sqrt(disc) / deno);
                    x2 = (-b / deno) - (Mathf.Sqrt(disc) / deno);
                    return new[] { x1, x2 };

                case 0:
                    x1 = -b / deno;
                    return new[] { x1 };

                default:
                    return new float[] { };
            }
        }

        private void DebugDrawLine(Vector3 aimingAt)
        {
            Vector3 pivot = pivotPoint.position;
            Debug.DrawLine(pivot, aimingAt, Color.yellow);
            Debug.DrawLine(pivot, Target.HeadshotPosition, Color.cyan);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pivotPoint.position, Data.AggroRadius);
        }
    }
}