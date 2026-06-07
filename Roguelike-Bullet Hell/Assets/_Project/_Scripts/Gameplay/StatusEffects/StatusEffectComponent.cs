using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using NUnit.Framework.Internal;
using Project.Gameplay.Health;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(HealthComponent))]
    public class StatusEffectComponent : MonoBehaviour
    {
        private readonly List<StatusEffect> statusEffects = new();
        private HealthComponent health;

        private void Awake()
        {
            health = GetComponent<HealthComponent>();
        }

        private void Update()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                StatusEffect effect = statusEffects[i];

                if (effect.IsExpired)
                {
                    OnStatusEffectExpired(effect);
                    continue;
                }

                effect.OnTick();
            }
        }

        public void AddStatusEffect(StatusEffect effect)
        {
            StatusEffect exisiting = statusEffects.OfType<StatusEffect>().FirstOrDefault();

            if(exisiting == null)
            {
                OnStatusEffectAdded(effect);
                return;
            }

            exisiting.Reapply(effect);
        }

        public void Clear()
        {
            foreach (StatusEffect effect in statusEffects)
            {
                effect.OnExpired();
            }

            statusEffects.Clear();
        }

        public bool TryGetStatusEffect<T>(out T effect) where T : StatusEffect
        {
            effect = statusEffects.OfType<T>().FirstOrDefault();

            return effect != null;
        }

        public bool HasStatusEffect<T>() where T : StatusEffect
        {
            return statusEffects.Any(effect => effect is T);
        }

        public void RemoveStatusEffect<T>() where T : StatusEffect
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                if (statusEffects[i] is T)
                {
                    statusEffects[i].OnExpired();
                    statusEffects.RemoveAt(i);
                }
            }
        }

        private void OnStatusEffectAdded(StatusEffect statusEffect)
        {
            statusEffect.OnApplied();
            statusEffects.Add(statusEffect);
        }

        private void OnStatusEffectExpired(StatusEffect statusEffect)
        {
            statusEffect.OnExpired();
            statusEffects.Remove(statusEffect);
        }

        private void OnEnable()
        {
            health.OnDeath += Clear;
        }

        private void OnDisable()
        {
            health.OnDeath -= Clear;
        }
    }
}