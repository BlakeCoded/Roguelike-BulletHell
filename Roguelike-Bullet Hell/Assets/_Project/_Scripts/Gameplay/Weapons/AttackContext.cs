using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class AttackContext
    {
        public float Damage;
        public float AttacksPerSecond;
        public float CritChance;
        public float CritDamage;
        public float ProjectileCount;
        public float ProjectileSpeed;
        public float Size;
        public float Knockback;
        public float LifeTime;
        public Transform owner;
    }
}