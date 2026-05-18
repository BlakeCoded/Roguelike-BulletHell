using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerCombat combat;

        private void Awake()
        {
            input = GetComponent<PlayerInput>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerCombat>();

            input.Initialize();
            movement.Initialize();
            combat.Initialize();
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
            
        }

        private void OnDisable()
        {
            
        }

        public void EquipWeapon(WeaponData weapon)
        {
            combat.EquipWeapon(weapon);
        }
    }
}