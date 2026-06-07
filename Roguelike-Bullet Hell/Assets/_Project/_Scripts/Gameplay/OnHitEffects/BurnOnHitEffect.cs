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

        public string Id => "Burn";
        public StackRule StackRule => StackRule.Stackable;

        public void Apply(DamageResult result, int count)
        {
            CombatContext combatContext = result.CombatContext;

            float damage = result.DamageDealt * 0.1f * count;

            BurnStatusEffect burn = new BurnStatusEffect(combatContext, damage, duration, tickRate);

            combatContext.Target.StatusEffects.AddStatusEffect(burn);
        }
    }
}