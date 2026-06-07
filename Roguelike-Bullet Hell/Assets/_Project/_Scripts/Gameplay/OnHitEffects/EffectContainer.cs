using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class EffectContainer
    {
        //public List<OnHitEffectEntry> Effects => effects;
        private readonly List<OnHitEffectEntry> effects = new();

        public void Add(IOnHitEffect effect)
        {
            OnHitEffectEntry existing = Find(effect);

            if(existing == null)
            {
                effects.Add(new OnHitEffectEntry(effect));
                return;
            }

            switch(effect.StackRule)
            {
                case StackRule.Unique:
                    return;
                    
                case StackRule.Stackable:
                    existing.AddStack();
                    break;
            }
        }

        public List<OnHitEffectEntry> CreateSnapshot()
        {
            return new List<OnHitEffectEntry>(effects);
        }

        private OnHitEffectEntry Find(IOnHitEffect effect)
        {
            return effects.FirstOrDefault(x => x.Effect.GetType() == effect.GetType());
        }
    }

    public enum StackRule
    {
        Unique,
        Stackable
    }

    public class OnHitEffectEntry
    {
        public IOnHitEffect Effect { get; }
        public int Count { get; private set; }

        public OnHitEffectEntry(IOnHitEffect effect)
        {
            Effect = effect;
            Count = 1;
        }

        public OnHitEffectEntry(IOnHitEffect effect, int count)
        {
            Effect = effect;
            Count = count;
        }

        public void AddStack()
        {
            Count++;
        }

        public void RemoveStack()
        {
            Count--;

            if(Count < 0) Count = 0; 
        }
    }
}