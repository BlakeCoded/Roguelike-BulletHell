using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerHealth health;
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            health = GetComponent<PlayerHealth>();
            health.Initialize();
            input = GetComponent<PlayerInput>();
            input.Initialize();
            movement = GetComponent<PlayerMovement>();
            movement.Initialize();
        }

        private void Update()
        {
            input.PollPlayerInput();
        }

        private void FixedUpdate()
        {
            movement.Move(input.MoveInput);
        }

        void OnDeath()
        {
            movement.CanMove = false;
        }

        private void OnEnable()
        {
            health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            health.OnDeath -= OnDeath;
        }

        public void TakeDamage(float amount)
        {
            health.TakeDamage(amount);
        }
    }
}