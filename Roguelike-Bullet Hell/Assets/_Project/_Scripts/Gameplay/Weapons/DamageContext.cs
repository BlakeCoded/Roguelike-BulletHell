using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct DamageContext
    {
        // Ownership
        public CombatContext CombatContext { get; }

        // Damage
        public float Damage { get; }
        public List<OnHitEffectEntry> OnHitEffects { get; }

        // Crit
        public bool IsCrit { get; }

        // Hit Info
        public Vector3 HitPosition { get; }
        public Vector3 HitDirection { get; }

        // Physics
        public float Knockback { get; }

        public DamageContext(
            CombatContext CombatContext,
            float Damage,
            List<OnHitEffectEntry> OnHitEffects,
            bool IsCrit,
            Vector3 HitPosition,
            Vector3 HitDirection,
            float Knockback)
        {
            this.CombatContext = CombatContext;
            this.Damage = Damage;
            this.OnHitEffects = OnHitEffects;
            this.IsCrit = IsCrit;
            this.HitPosition = HitPosition;
            this.HitDirection = HitDirection;
            this.Knockback = Knockback;
        }

        public static DamageContext Create(DamageContext context, float damage)
        {
            return new DamageContext(
                context.CombatContext,
                damage,
                DamageResolver.EmptyOnHitEffects,
                false,
                context.HitPosition,
                context.HitDirection,
                0f);
        }
    }
}