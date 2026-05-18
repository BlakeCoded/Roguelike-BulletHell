using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Health
{
    public class HealthComponent : MonoBehaviour, IInitializable
    {
        public float CurrentHealth => currentHealth;
        private float currentHealth;
        public float MaxHealth => maxHealth;
        [SerializeField] private float maxHealth;
        public bool IsAlive => isAlive;
        public bool IsInitialized { get; private set; }
        private bool isAlive;

        public Action OnDeath;

        public void Initialize()
        {
            IsInitialized = true;
            isAlive = true;
            currentHealth = maxHealth;
            OnDeath += Die;
        }

        public void TakeDamage(float amount)
        {
            if (!isAlive) return;

            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            OnDeath?.Invoke();
        }

        private void OnDisable()
        {
            OnDeath -= Die;
        }
    }
}