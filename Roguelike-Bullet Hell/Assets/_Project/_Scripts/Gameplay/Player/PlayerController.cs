using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerWeapons combat;

        private HealthComponent health;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerWeapons>();
            health = GetComponent<HealthComponent>();
        }

        private void Update()
        {
            input.PollPlayerInput();

            movement.Move(input.MoveInput);
            movement.RotateToMousePosition(input.MousePosition);

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