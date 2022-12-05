using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct EnemyData
    {
        public const float DefaultReachDistance = .05f;

        [Min(0)] public float hp;
        [HideInInspector] public float currentHp;
        [Min(0)] public float speed;
        [Min(DefaultReachDistance)] public float reachDistance;
    }
}