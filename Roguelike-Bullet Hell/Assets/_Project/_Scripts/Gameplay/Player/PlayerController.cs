using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Health;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerCombat combat;

        private HealthComponent health;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerCombat>();
            health = GetComponent<HealthComponent>();
        }

        private void Update()
        {
            input.PollPlayerInput();

            movement.Move(input.MoveInput);

            if (input.FirePressed)
            {
                combat.Attack();
            }
        }

        // Use for physics based movement
        private void FixedUpdate()
        {
            
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
    }
}