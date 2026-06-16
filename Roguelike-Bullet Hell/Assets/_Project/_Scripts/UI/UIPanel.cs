using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Gameplay.UI
{
    /// <summary>
    /// Represents a single UI window or screen.
    /// Handles opening, closing, visibility state, animations, and sizing.
    /// </summary>
    public abstract class UIPanel : MonoBehaviour
    {
        public virtual bool AddToBackStack => true;
        public bool IsOpen => gameObject.activeSelf;

        public event Action OnOpened;
        public event Action OnClosed;

        protected virtual void Start() 
        {
            if(AddToBackStack && IsOpen)
            {
                UIManager.Open(this);
            }
        }

        public virtual void Open()
        {
            if (IsOpen) return;

            gameObject.SetActive(true);

            OnOpen();

            OnOpened?.Invoke();
        }

        public virtual void Close()
        {
            if(!IsOpen) return;

            OnClose();

            gameObject.SetActive(false);

            OnClosed?.Invoke();
        }

        protected void Toggle()
        {
            if(IsOpen)
            {
                UIManager.Close(this);
                return;
            }

            UIManager.Open(this);
        }

        protected virtual void OnOpen() { } 
        protected virtual void OnClose() { } 
    }
}