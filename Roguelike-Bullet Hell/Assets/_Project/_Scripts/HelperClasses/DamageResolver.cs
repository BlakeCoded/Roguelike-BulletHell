using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public static class DamageResolver
    {
        public static DamageContext CreateDamageContext(AttackContext context, Entity Target, Vector3 hitPosition, Vector3 hitDirection)
        {
            bool isCrit = Random.value < context.CritChance / 100f;

            float damage = isCrit ? context.Damage * context.CritDamage : context.Damage;

            CombatContext combatContext = new(context.Owner, Target);

            return new DamageContext(combatContext, damage, context.OnHitEffects, isCrit, hitPosition, hitDirection, context.Knockback);
        }

        //public static DamageContext ProcessHit(AttackContext context, Entity target, Vector3 hitPosition, Vector3 hitDirection)
        //{
        //    DamageContext damageContext = CreateDamageContext(context, target, hitPosition, hitDirection);

        //    target.Health.TakeDamage(damageContext);

        //    foreach(IOnHitEffect effect in damageContext.OnHitEffects)
        //    {
        //        effect.Apply(damageContext);
        //    }

        //    return damageContext;
        //}

        public static void ProcessHit(DamageContext context)
        {
            Entity target = context.CombatContext.Target;

            DamageResult result = target.Health.TakeDamage(context);

            if (result.DamageDealt <= 0) return; // change this later :)

            // When Entity holds a movement component call knockback here 
            if (context.Knockback > 0f)
            {
                // apply knockback
            }

            foreach (IOnHitEffect effect in context.OnHitEffects)
            {
                effect.Apply(context, result);
            }

            GameTextManager.Instance.ShowDamage(result, context.HitPosition);
        }
    }
}