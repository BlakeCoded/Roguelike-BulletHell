using System;
using System.Collections;
using System.Collections.Generic;
using Project.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Systems.Keybinds
{
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {
        public event Action<string> OnActionPreformed;

        private PlayerControls input;
        private Dictionary<string, InputAction> actions;

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

        private void OnEnable()
        {
            input.Enable();

            foreach (var action in actions.Values)
            {
                action.performed += HandleAction;
            }
        }

        private void OnDisable()
        {
            foreach (var action in actions.Values)
            {
                action.performed -= HandleAction;
            }

            input.Disable();
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
            Instance.OnActionPreformed += callback;
        }
        private void InternalUnsubscribe(Action<string> callback)
        {
            Instance.OnActionPreformed -= callback;
        }
    }
}