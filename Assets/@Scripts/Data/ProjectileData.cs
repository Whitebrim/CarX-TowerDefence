using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct ProjectileData
    {
        [Min(0)] public float speed;
        [Min(0)] public float damage;
    }
}