using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public abstract class StatusEffect
    {
        protected Entity Owner { get; set; }
        protected Entity Target { get; set; }
        protected float Damage { get; set; }
        protected float Duration { get; set; }
        protected float TickRate { get; set; }

        protected StatusEffect(Entity owner, Entity target, float damage, float duration, float tickRate)
        {
            Owner = owner;
            Target = target;
            Damage = damage;
            Duration = duration;
            TickRate = tickRate;
        }

        public bool IsExpired => Duration <= 0;

        public virtual void OnApplied() { }
        public virtual void OnTick() { }
        public virtual void OnExpired() { }
        public abstract void Refresh(float damage, float duration);
    }
}