using System;
using UnityEngine;

[Serializable]
public struct EnemyData
{
    public const float DefaultReachDistance = .05f;

    public float hp;
    [HideInInspector] public float currentHp;
    public float speed;
    [Min(DefaultReachDistance)] public float reachDistance;

    public EnemyData(float hp, float speed, float reachDistance = DefaultReachDistance)
    {
        this.hp = hp;
        this.currentHp = hp;
        this.speed = speed;
        this.reachDistance = reachDistance;
    }
}