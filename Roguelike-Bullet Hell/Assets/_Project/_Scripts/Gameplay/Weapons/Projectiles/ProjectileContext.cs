using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public struct ProjectileContext
    {
        // Ownership
        public GameObject Owner { get; }

        // Damage
        public float Damage { get; }

        // Crit
        public float CritChance { get; }
        public float CritDamage { get; }

        // Movement
        public Vector3 Direction { get; }
        public float LifeTime { get; }

        // Collision
        public float Size { get; }

        // Behaviours
        public int Pierce { get; }
        public int Bounce { get; }

        // Effects
        public float Knockback { get; }

        // Utility
        public bool CanCrit { get; }

        public ProjectileContext(
            GameObject Owner,
            float Damage,
            float CritChance,
            float CritDamage,
            Vector3 Direction,
            float LifeTime,
            float Size,
            int Pierce,
            int Bounce,
            float Knockback,
            bool CanCrit)
        {
            this.Owner = Owner;
            this.Damage = Damage;
            this.CritChance = CritChance;
            this.CritDamage = CritDamage;
            this.Direction = Direction;
            this.LifeTime = LifeTime;
            this.Size = Size;
            this.Pierce = Pierce;
            this.Bounce = Bounce;
            this.Knockback = Knockback;
            this.CanCrit = CanCrit;
        }
    }
}