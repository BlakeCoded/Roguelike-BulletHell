using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Combat
{
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

            DamageResult result = target.Health.TakeDamage(context); // calculate damage with armor / resistances etc..

            if (result.DamageDealt <= 0) return;

            if (context.Knockback > 0f)
            {
                // apply knockback
            }

            foreach (OnHitEffectEntry entry in context.OnHitEffects)
            {
                entry.Effect.Apply(result, entry.Count);
            }

            GameTextManager.SpawnDamageUiText(result, context.HitPosition);
        }

        public static void ProcessDamage(DamageContext context)
        {
            CombatEntity target = context.CombatContext.Target;

            DamageResult damageResult = target.Health.TakeDamage(context);

            GameTextManager.SpawnDamageUiText(damageResult, context.HitPosition);
        }
    }
}