using System;
using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Player;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class BurnStatusEffect : DamageOverTimeEffect
    {
        public int Stacks { get; private set; }

        public BurnStatusEffect(CombatContext combatContext, float damage, float duration, float tickRate, DamageType damageType = DamageType.Fire): base(combatContext, damage, duration, tickRate, damageType) { }

        private const int STACKS_TO_IGNITE = 7;

        public override void Reapply(StatusEffect effect)
        {
            if (effect is not BurnStatusEffect burn) return;

            Damage = burn.BaseDamage;

            Stacks++;

            if(Stacks >= STACKS_TO_IGNITE)
            {
                Ignite();
                return;
            }

            Duration = burn.BaseDuration;
        }

        public override void OnApplied()
        {
            base.OnApplied();
            Stacks = 1;
        }

        public override void OnTick()
        {
            base.OnTick();
        }

        public override void OnExpired()
        {
            base.OnExpired();
        }

        private void Ignite()
        {
            float ignite = BaseDamage * Stacks;

            DamageResolver.ProcessDamage(DamageContext.CreateSimpleDamageContext(CombatContext, ignite, DamageType));

            Duration = 0f;
        }
    }
}