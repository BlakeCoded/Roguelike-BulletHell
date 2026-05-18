using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Health
{
    [RequireComponent(typeof(StatsComponent))]
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        public float CurrentHealth => currentHealth;
        private float currentHealth;
        public float MaxHealth => stats.GetStatValue(StatType.Health);
        public bool IsAlive => isAlive;
        private bool isAlive;

        public Action OnDeath;

        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();
        }

        private void Start()
        {
            isAlive = true;
            currentHealth = MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            if (!IsAlive) return;

            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die();
            }
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
    }
}