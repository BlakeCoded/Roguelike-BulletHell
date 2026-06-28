using Project.Gameplay.Combat;
using Collision;
using Interfaces;
using UnityEngine;

namespace Project.Player
{
    public class PlayerEntity : CombatEntity
    {
        public PlayerInput Input { get; private set; }
        public PlayerCameraController CameraController { get; private set; }
        public PlayerWeaponHolder PlayerWeapons { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Input = GetComponent<PlayerInput>();
            CameraController = GetComponent<PlayerCameraController>();
            PlayerWeapons = GetComponent<PlayerWeaponHolder>();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void Update()
        {
            Input.PollPlayerInput();

            Movement.Move(Input.MoveInput);

            SyncCollisionTransform();

            if (Input.FirePressed)
            {
                PlayerWeapons.Attack();
            }
        }

        private void LateUpdate()
        {
            CameraController.CameraUpdate(Input.MouseDelta);
        }

        // Use for physics based updates
        private void FixedUpdate()
        {
            
        }

        void OnDeath()
        {
            Movement.CanMove = false;
        }

        public override void OnHit(CollisionObject other)
        {
            
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