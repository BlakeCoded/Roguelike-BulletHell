using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Handles combat damage calculation and converts attack data into
    /// a final DamageResult that can be applied to a target.
    /// </summary>
    public static class DamageResolver
    {
        public static readonly List<OnHitEffectEntry> EmptyOnHitEffects = new();

        public static DamageContext CreateDamageContext(AttackContext context, CombatEntity Target, Vector3 hitPosition, Vector3 hitDirection)
        {
            bool isCrit = Random.value < context.CritChance * 0.01f;

            float damage = isCrit ? context.Damage * context.CritMultiplier : context.Damage;

            CombatContext combatContext = new(context.Owner, Target);

            return new DamageContext(combatContext, context.Damage, damage, DamageType.Physical, context.OnHitEffects, isCrit, hitPosition, hitDirection, context.Knockback);
        }

        public static void ProcessHit(DamageContext context)
        {
            CombatEntity target = context.CombatContext.Target;

            DamageResult damageResult = target.Health.TakeDamage(context); // calculate damage with armor / resistances etc..

            if (damageResult.DamageDealt <= 0f) return;

            if (context.Knockback > 0f)
            {
                // apply knockback
            }

            foreach (OnHitEffectEntry entry in context.OnHitEffects)
            {
                entry.Effect.Apply(damageResult, entry.Count);
            }

            CombatEvents.RaiseDamageDealt(damageResult, context.HitPosition);
        }

        public static void ProcessDamage(DamageContext context)
        {
            CombatEntity target = context.CombatContext.Target;

            DamageResult damageResult = target.Health.TakeDamage(context);

            CombatEvents.RaiseDamageDealt(damageResult, context.HitPosition);
        }
    }
}