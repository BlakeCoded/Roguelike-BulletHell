using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public abstract class IOnHitEffect
    {
        public object Source { get; }
        public StackRule StackRule { get; }

        public IOnHitEffect(object source, StackRule stackRule)
        {
            Source = source;
            StackRule = stackRule;
        }

        public abstract void Apply(DamageResult result, int count);
    }

    public abstract class IOnHitEffectData : ScriptableObject
    {
        public abstract StackRule StackRule { get; }
        public abstract IOnHitEffect Create(object Source);
    }

    public abstract class DamageOverTimeOnHitEffect : IOnHitEffect
    {
        protected DamageOverTimeOnHitEffect(object source, StackRule stackRule) : base(source, stackRule) { }
        protected abstract float Duration { get; }
        protected abstract float TickRate { get; }
        public abstract override void Apply(DamageResult result, int count);
    }
}