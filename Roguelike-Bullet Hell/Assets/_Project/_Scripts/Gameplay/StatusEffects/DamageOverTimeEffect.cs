using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public abstract class DamageOverTimeEffect : StatusEffect
    {
        public float Damage;
        protected float BaseDamage;
        protected float TickRate;

        private float timer;

        protected DamageOverTimeEffect(
            CombatContext context, 
            float damage, 
            float duration, 
            float tickRate) : base(context, duration)
        {
            Damage = damage;
            BaseDamage = damage;
            TickRate = tickRate;
        }

        public override void OnTick()
        {
            if(IsExpired) return;

            float dt = GameTime.DeltaTime;

            Duration -= dt;
            timer += dt;

            if(timer > TickRate)
            {
                ApplyDamageTick();
                timer = 0;
            }
        }

        public virtual void ApplyDamageTick()
        {
            DamageResolver.ProcessHit(
                new DamageContext(
                    CombatContext,
                    Damage,
                    DamageResolver.EmptyOnHitEffects,
                    false,
                    CombatContext.Target.transform.position,
                    Vector3.zero,
                    0));
        }
    }
}