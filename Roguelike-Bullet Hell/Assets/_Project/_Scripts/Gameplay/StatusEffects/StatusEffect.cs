using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public abstract class StatusEffect
    {
        protected CombatContext CombatContext { get; set; }
        public float Duration { get; set; }
        protected float BaseDuration {  get; set; }

        protected StatusEffect(CombatContext combatContext, float duration)
        {
            CombatContext = combatContext;
            Duration = duration;
            BaseDuration = duration;
        }

        public bool IsExpired => Duration <= 0;

        public virtual void OnApplied() { }
        public virtual void OnTick() { }
        public virtual void OnExpired() { }
        public abstract void Reapply(StatusEffect effect);
    }
}