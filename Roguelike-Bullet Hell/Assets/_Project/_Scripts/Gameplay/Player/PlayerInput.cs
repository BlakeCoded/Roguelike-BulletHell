using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour, IInitializable
    {
        private PlayerControls inputActions;
        public Vector2 MoveInput {  get; private set; }
        public bool FirePressed { get; private set; }
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
            inputActions = new PlayerControls();
            inputActions.Enable();
        }

        public void PollPlayerInput()
        {
            MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
            FirePressed = inputActions.Player.Fire.IsPressed();
        }
        
        private void OnDisable()
        {
            inputActions.Disable();
        }
    }
}