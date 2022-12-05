using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct TowerData
    {
        [Min(0)] public float ShootInterval;
        [Min(0)] public float AggroRadius;
    }
}