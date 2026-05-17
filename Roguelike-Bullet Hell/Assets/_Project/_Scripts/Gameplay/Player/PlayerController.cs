using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Player.Stats;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerCombat combat;

        //[SerializeField] private PlayerHealth health;
        //[SerializeField] private PlayerStats stats;

        private void Awake()
        {
            //health = GetComponent<PlayerHealth>();
            input = GetComponent<PlayerInput>();
            movement = GetComponent<PlayerMovement>();
            combat = GetComponent<PlayerCombat>();
            //stats = GetComponent<PlayerStats>();

            //health.Initialize();
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
            //health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            //health.OnDeath -= OnDeath;
        }

        public void TakeDamage(float amount)
        {
            //health.TakeDamage(amount);
        }

        public void EquipWeapon(WeaponData weapon)
        {
            combat.EquipWeapon(weapon);
        }
    }
}