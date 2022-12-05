using Data;
using UnityEngine;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Tower
{
    public abstract class Tower : MonoBehaviour
    {
        protected TowerData Data { get; private set; }
        [field: SerializeField] protected Transform ShootingPoint { get; private set; }

        protected void Constructor(Transform shootingPoint)
        {
            ShootingPoint = shootingPoint;
        }
    }
}