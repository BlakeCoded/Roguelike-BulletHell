using System;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Health
{
    [RequireComponent(typeof(StatsComponent))]
    [RequireComponent(typeof(StatsInitializer))]
    public class HealthComponent : MonoBehaviour, IDamageable, IHealable
    {
        private StatsComponent stats;
        public float CurrentHealth => currentHealth;
        private float currentHealth;
        public float MaxHealth => baseMaxHealth + stats.GetStatValue(StatType.BonusHealth);
        [SerializeField] private float baseMaxHealth = 100;
        public bool IsAlive => isAlive;
        private bool isAlive;

        // Events
        public event Action OnDamaged;
        public event Action OnDeath;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();
        }

        private void Start()
        {
            isAlive = true;
            currentHealth = MaxHealth;
        }

        public DamageResult TakeDamage(DamageContext context)
        {
            if (!IsAlive) return new DamageResult(context.CombatContext, 0, 0, false, false);

            float finalDamage = CalaculateDamage(context);

            currentHealth -= finalDamage;
            currentHealth = Mathf.Max(0f, currentHealth);

            OnDamaged?.Invoke();

            bool killedTarget = currentHealth <= 0f;

            if (killedTarget)
            {
                Die();
            }

            return new DamageResult(context.CombatContext, context.PreModifierDamage, finalDamage, context.IsCrit, killedTarget);
        }

        public void Heal(float amount)
        {
            if(!IsAlive) return;

            currentHealth += amount;

            currentHealth = Mathf.Min(currentHealth, MaxHealth);
        }

        void Die()
        {
            isAlive = false;

            OnDeath?.Invoke();
        }

        float CalaculateDamage(DamageContext context)
        {
            // if resistances calculate here

            return context.Damage;
        }
    }
}