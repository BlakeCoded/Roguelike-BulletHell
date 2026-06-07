using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Health;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Player
{
    public class PlayerController : Entity
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private CameraController cameraMove;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerWeapons combat;

        protected override void Awake()
        {
            base.Awake();

            input = GetComponent<PlayerInput>();
            cameraMove = GetComponent<CameraController>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerWeapons>();
        }

        private void Update()
        {
            input.PollPlayerInput();

            movement.Move(input.MoveInput);
            cameraMove.RotateToMousePosition(input.MousePosition);

            combat.Attack();

            if (input.FirePressed)
            {
                //combat.Attack();
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
            Health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            Health.OnDeath -= OnDeath;
        }
    }
}