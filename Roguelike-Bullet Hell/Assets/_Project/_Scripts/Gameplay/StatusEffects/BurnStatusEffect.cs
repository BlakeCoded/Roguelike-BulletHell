using System;
using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class BurnStatusEffect : DamageOverTimeEffect
    {
        public BurnStatusEffect(CombatContext combatContext, float damage, float duration, float tickRate): base(combatContext, damage, duration, tickRate) { }

        public override void Reapply(StatusEffect effect)
        {
            if (effect is not BurnStatusEffect burn) return;

            Duration = burn.BaseDuration;
            Damage = burn.Damage;
        }
    }
}