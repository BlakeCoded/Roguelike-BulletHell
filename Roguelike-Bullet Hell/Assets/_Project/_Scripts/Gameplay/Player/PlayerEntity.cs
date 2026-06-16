using Project.Gameplay.Combat;

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

        private void Update()
        {
            Input.PollPlayerInput();

            Movement.Move(Input.MoveInput);

            PlayerWeapons.Attack();

            if (Input.FirePressed)
            {
                //PlayerWeapons.Attack();
            }
        }

        private void LateUpdate()
        {
            CameraController.RotateAroundTarget(Input.MouseDelta);
        }

        // Use for physics based updates
        private void FixedUpdate()
        {
            
        }

        void OnDeath()
        {
            Movement.CanMove = false;
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