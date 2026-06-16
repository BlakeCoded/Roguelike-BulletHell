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
        public float PreModifierDamage { get; }
        public float Damage { get; }
        public DamageType DamageType { get; }
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
            float PreModifierDamage,
            float Damage,
            DamageType DamageType,
            List<OnHitEffectEntry> OnHitEffects,
            bool IsCrit,
            Vector3 HitPosition,
            Vector3 HitDirection,
            float Knockback)
        {
            this.CombatContext = CombatContext;
            this.PreModifierDamage = PreModifierDamage;
            this.Damage = Damage;
            this.DamageType = DamageType;
            this.OnHitEffects = OnHitEffects;
            this.IsCrit = IsCrit;
            this.HitPosition = HitPosition;
            this.HitDirection = HitDirection;
            this.Knockback = Knockback;
        }

        public static DamageContext CreateSimpleDamageContext(CombatContext context, float damage, DamageType damageType)
        {
            return new DamageContext(
                context,
                0f,
                damage,
                damageType,
                DamageResolver.EmptyOnHitEffects,
                false,
                context.Target.CachedTransform.position,
                Vector3.zero,
                0f);
        }
    }

    [System.Serializable]
    public enum DamageType
    {
        Physical,
        Fire,
        Water,
        Ice,
        Poison,
        Lightning,
        True
    }
}