using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerControls inputActions;

        public Vector2 MousePosition { get; private set; }
        public Vector2 MoveInput {  get; private set; }
        public bool FirePressed { get; private set; }

        private void Awake()
        {
            inputActions = new PlayerControls();
            inputActions.Enable();
        }

        public void PollPlayerInput()
        {
            MousePosition = inputActions.Player.MousePosition.ReadValue<Vector2>();
            MoveInput = inputActions.Player.Move.ReadValue<Vector2>();
            FirePressed = inputActions.Player.Fire.IsPressed();

            //Debug.Log(inputActions.Player.Move.activeControl);
        }
        
        private void OnDisable()
        {
            inputActions.Disable();
        }
    }
}