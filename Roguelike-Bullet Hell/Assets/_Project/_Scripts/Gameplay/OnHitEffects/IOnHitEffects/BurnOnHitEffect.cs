using System.Collections;
using System.Collections.Generic;
using OnHitEffect;
using Project.Gameplay.Combat;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class BurnOnHitEffect : DamageOverTimeOnHitEffect
    {
        public BurnOnHitEffect(object source, StackRule stackRule) : base(source, stackRule) { }

        protected override float Duration => 5f;
        protected override float TickRate => 1f;

        public override void Apply(DamageResult result, int count)
        {
            CombatContext combatContext = result.CombatContext;

            float damage = result.PreModifierDamage * 0.1f * count;

            if(damage <= 1f) damage = 1f;

            BurnStatusEffect burn = new BurnStatusEffect(combatContext, damage, Duration, TickRate);

            combatContext.Target.StatusEffects.AddStatusEffect(burn);
        }
    }
}