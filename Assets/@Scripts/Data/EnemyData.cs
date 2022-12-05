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

        public EnemyData(float hp, float speed, float reachDistance = DefaultReachDistance)
        {
            this.hp = hp;
            this.currentHp = hp;
            this.speed = speed;
            this.reachDistance = reachDistance;
        }
    }
}