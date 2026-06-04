using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class BurnStatusEffect : StatusEffect
    {
        private float timer;

        public BurnStatusEffect(Entity owner, Entity target, float damage, float duration, float tickRate): base(owner, target, damage, duration, tickRate) { }

        public override void Refresh(float damage, float duration)
        {
            Duration = duration;
            Damage = Damage + damage;
        }

        public override void OnApplied()
        {
            timer = 0f;
        }

        public override void OnTick()
        {
            if (IsExpired) return;

            float dt = GameTime.DeltaTime;

            timer += dt;

            Duration -= dt;

            if(timer > TickRate)
            {
                DamageResolver.ProcessHit(new DamageContext(
                    new CombatContext(Owner, Target),
                    Damage,
                    new(),
                    false,
                    Target.transform.position,
                    Vector3.zero,
                    0
                    ));

                timer = 0;
            } 
        }

        public override void OnExpired()
        {
            
        }
    }
}