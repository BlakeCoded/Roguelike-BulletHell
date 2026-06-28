using Collision;
using UnityEngine;
using System.Collections.Generic;
using static Helper;

namespace Project.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        private Camera mainCamera;

        [Header("CAMERA_PLAYER")]
        [SerializeField] private Transform focusTarget;
        [SerializeField] private Transform playerBody;
        [SerializeField] private float distanceFromTarget;
        [SerializeField] private float cameraSensitivity;
        [SerializeField] private float minPitch = -30f;
        [SerializeField] private float maxPitch = 80f;
        [SerializeField] private float cameraSmooth = 15f;
        [SerializeField] private float cameraRadius = 0.25f;
        [SerializeField] private LayerMask cameraCollisionMask;

        [Header("CAMERA_CROSSHAIR")]
        [SerializeField] private Transform firePosition;
        [SerializeField] private float maxAimDistance;
        [SerializeField] private float aimAssistDistance = 50f;
        [SerializeField] private float minimumAimDistance;
        [SerializeField] private float aimSmoothing;
        [SerializeField] private LayerMask aimMask;

        private float yaw;
        private float pitch;

        private void Awake()
        {
            mainCamera = MainCamera;
        }

        public void CameraUpdate(Vector2 inputVector)
        {
            RotateCameraAroundTarget(inputVector);

            UpdateWeaponAim();

            //if (GameManager.Raycast(mainCamera.transform.position, mainCamera.transform.forward, 50f, CollisionLayer.Player, out RaycastHitData hit))
            //{
            //    Debug.Log($"Hit: {hit.CollisionObject.Entity.name}");
            //}
        }

        public void RotateCameraAroundTarget(Vector2 inputVector)
        {
            yaw += inputVector.x * cameraSensitivity;
            pitch -= inputVector.y * cameraSensitivity;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 offset = rotation * new Vector3(0, 0, -distanceFromTarget);

            Vector3 desiredPos = focusTarget.position + offset;

            Vector3 direction = (desiredPos - focusTarget.position).normalized;
            float distance = Vector3.Distance(focusTarget.position, desiredPos);

            if(Physics.SphereCast(
                focusTarget.position,
                cameraRadius,
                direction,
                out RaycastHit hit,
                distance,
                cameraCollisionMask))
            {
                desiredPos = hit.point + hit.normal * cameraRadius;
            }

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, desiredPos, cameraSmooth * GameTime.DeltaTime);
            mainCamera.transform.LookAt(focusTarget.position);

            RotateBodyToCameraRotation();
        }

        private void RotateBodyToCameraRotation()
        {
            Vector3 cameraForward = mainCamera.transform.forward;

            cameraForward.y = 0f;

            if (cameraForward.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

            playerBody.rotation = targetRotation;
        }

        private Vector3 GetAimPoint()
        {
            Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            Vector3 aimPoint = ray.origin + ray.direction * aimAssistDistance;

            if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance, aimMask))
            {
                float distanceFromFirepoint = Vector3.Distance(ray.origin, hit.point);

                if (distanceFromFirepoint > minimumAimDistance)
                {
                    aimPoint = hit.point;
                }
                else
                {
                    aimPoint = ray.origin + ray.direction * minimumAimDistance;
                }
            }

            return aimPoint;
        }

        private Vector3 currentAimPoint;

        private void UpdateWeaponAim()
        {
            Vector3 targetAimPoint = GetAimPoint();

            currentAimPoint = Vector3.Lerp(currentAimPoint, targetAimPoint, aimSmoothing * GameTime.DeltaTime);

            Vector3 direction = currentAimPoint - firePosition.position;

            if (direction.sqrMagnitude < 0.001f)
                return;

            firePosition.rotation = Quaternion.LookRotation(direction);
        }
    }
}