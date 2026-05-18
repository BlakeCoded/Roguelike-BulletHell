using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerControls inputActions;
        public Vector2 MoveInput {  get; private set; }
        public bool FirePressed { get; private set; }

        private void Awake()
        {
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