using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct AttackContext
    {
        // Ownership
        public Entity Owner { get; }

        // Core Damage
        public float Damage { get; }
        public List<OnHitEffectEntry> OnHitEffects { get; }

        // Crit
        public float CritChance { get; }
        public float CritDamage { get; }
        
        // Projectile Creation
        public int ProjectileCount { get; }

        // General Attack Scaling
        public float Size { get; }

        // Utility
        public float Knockback { get; }
        

        public AttackContext(
            Entity Owner,
            float Damage,
            List<OnHitEffectEntry> OnHitEffects,
            float CritChance,
            float CritDamage,
            int ProjectileCount,
            float Size,
            float Knockback)
        {
            this.Owner = Owner;
            this.Damage = Damage;
            this.OnHitEffects = OnHitEffects;
            this.CritChance = CritChance;
            this.CritDamage = CritDamage;
            this.ProjectileCount = ProjectileCount;
            this.Size = Size;
            this.Knockback = Knockback;
        }
    }
}