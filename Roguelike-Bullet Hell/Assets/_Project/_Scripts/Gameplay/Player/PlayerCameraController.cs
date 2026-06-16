using UnityEngine;
using static Helper;

namespace Project.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private float distanceFromTarget;
        [SerializeField] private float cameraSensitivity;
        [SerializeField] private float minPitch = -30f;
        [SerializeField] private float maxPitch = 80f;
        [SerializeField] private Transform focusTarget;

        private float yaw;
        private float pitch;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = MainCamera;
        }

        public void RotateAroundTarget(Vector2 inputVector)
        {
            yaw += inputVector.x * cameraSensitivity;
            pitch -= inputVector.y * cameraSensitivity;

            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

            Vector3 offset = rotation * new Vector3(0, 0, -distanceFromTarget);

            mainCamera.transform.position = focusTarget.position + offset;
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

            focusTarget.rotation = Quaternion.Slerp(focusTarget.rotation, targetRotation, 50f * GameTime.DeltaTime);
        }


        public void RotateToMousePosition(Vector2 position)
        {
            Ray ray = MainCamera.ScreenPointToRay(position);

            Plane groundPlane = new Plane(Vector3.up, focusTarget.position);

            if(groundPlane.Raycast(ray, out float distance))
            {
                Vector3 worldPoint = ray.GetPoint(distance);

                Vector3 direction = worldPoint - focusTarget.position;

                direction.y = 0;

                if (direction.sqrMagnitude < 0.0001f) return;

                Quaternion targetRotation = Quaternion.LookRotation(direction);

                focusTarget.rotation = targetRotation;
            }
        }
    }
}