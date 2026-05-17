using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour, IMovement, IInitializable
    {
        public bool CanMove { get; set; }
        public bool IsInitialized { get; private set; }

        [SerializeField] float moveSpeed;

        public void Move(Vector2 direction)
        {
            if (CanMove == false || direction == Vector2.zero) return;

            transform.MoveByXZ(direction * moveSpeed * Time.deltaTime);
        }

        public void Initialize()
        { 
            CanMove = true;



            IsInitialized = true;
        }
    }
}