using System;
using System.Collections.Generic;
using Project.Singleton;
using UnityEngine.InputSystem;

namespace Project.Systems.Keybinds
{
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {
        private event Action<string> OnActionPreformed;
        private Dictionary<string, InputAction> actions;
        private PlayerControls input;

        private void Awake()
        {
            input = new PlayerControls();

            actions = new Dictionary<string, InputAction>();

            foreach(var action in input.asset)
            {
                actions[action.name] = action;
            }
        }

        private void HandleAction(InputAction.CallbackContext context)
        {
            OnActionPreformed?.Invoke(context.action.name);
        }

        public static void Subscribe(Action<string> callback)
        {
            Instance.InternalSubscribe(callback);
        }
        public static void Unsubscribe(Action<string> callback)
        {
            if(Instance == null) return;

            Instance.InternalUnsubscribe(callback);
        }

        private void InternalSubscribe(Action<string> callback)
        {
            OnActionPreformed += callback;
        }
        private void InternalUnsubscribe(Action<string> callback)
        {
            OnActionPreformed -= callback;
        }

        private void OnEnable()
        {
            input.Enable();

            foreach (var action in actions.Values)
            {
                action.performed += HandleAction;
            }
        }
        private void OnDestroy()
        {
            foreach (var action in actions.Values)
            {
                action.performed -= HandleAction;
            }

            input.Disable();
        }
    }
}