using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;
using static Helper;

namespace Project.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform playerBody;

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