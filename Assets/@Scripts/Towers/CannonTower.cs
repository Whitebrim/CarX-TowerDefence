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
            Vector3 aimTo = PredictInterception();
            Debug.DrawLine(ShootingPoint.position, aimTo, Color.yellow);
            Vector3 lookDirection = aimTo + Vector3.up - PivotPoint.position;
            Debug.DrawLine(PivotPoint.position, Target.Position, Color.cyan);
            Yaw.localRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));
            Pitch.localRotation = Quaternion.LookRotation(new Vector3(0, lookDirection.y, lookDirection.z));
        }


        /// <summary>
        /// <see href="https://www.reddit.com/r/gamedev/comments/16ceki/comment/c7vbu2j"/> 
        /// </summary>
        /// <returns></returns>
        private Vector3 PredictInterception()
        {
            Vector3 v = Target.Forward * Target.Speed;
            Vector3 w = Target.Position - PivotPoint.position;
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
            return Target.Position + v * x;
        }

        private float[] SolveQuadratic(float a, float b, float c)
        {
            float disc, deno, x1, x2;
            if (a == 0)
            {
                x1 = -c / b;
                return new[] { x1 };
            }

            disc = (b * b) - (4 * a * c);
            deno = 2 * a;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(PivotPoint.position, Data.AggroRadius);
        }
    }
}