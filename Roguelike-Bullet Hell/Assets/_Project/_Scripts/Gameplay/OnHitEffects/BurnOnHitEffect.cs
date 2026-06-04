using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class BurnOnHitEffect : IOnHitEffect
    {
        private readonly float duration = 5f;
        private readonly float tickRate = 1f;

        public void Apply(DamageContext context, DamageResult result)
        {
            CombatContext combatContext = result.CombatContext;

            float damage = result.DamageDealt * 0.1f;

            if (combatContext.Target.StatusEffects.TryGetStatusEffect<BurnStatusEffect>(out BurnStatusEffect effect))
            {
                effect.Refresh(damage, duration);
                return;
            }

            BurnStatusEffect burn = new BurnStatusEffect(combatContext.Owner, combatContext.Target, damage, duration, tickRate);

            combatContext.Target.StatusEffects.AddStatusEffect(burn);
        }
    }
}