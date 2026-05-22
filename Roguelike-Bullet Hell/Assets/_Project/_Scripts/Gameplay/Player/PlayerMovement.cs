using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;
using static Helper;

namespace Project.Player
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        [SerializeField] Transform playerBody;
        [SerializeField] float baseMoveSpeed = 5;
        private float MoveSpeed => baseMoveSpeed + stats.GetStatValue(StatType.MoveSpeed);
        public bool CanMove { get; set; }

        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();
        }

        private void Start()
        {
            CanMove = true;
        }

        public void Move(Vector2 direction)
        {
            if (CanMove == false || direction == Vector2.zero) return;

            transform.MoveByXZ(MoveSpeed * Time.deltaTime * direction);
        }

        public void RotateToMousePosition(Vector2 position)
        {
            Ray ray = MainCamera.ScreenPointToRay(position);

            Plane groundPlane = new Plane(Vector3.up, playerBody.position);

            if(groundPlane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);

                Vector3 direction = worldPoint - playerBody.position;

                direction.y = 0;

                if (direction.sqrMagnitude < 0.0001f) return;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                playerBody.rotation = targetRotation;
            }
        }
    }
}