using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    [System.Serializable]
    public struct CollisionShape
    {
        public ShapeType Type;

        public float Radius;        // sphere
        public Vector3 HalfExtents; // box
        public float Height;        // capsule

        public void GetBounds(Vector3 position, Quaternion rotation, out Vector3 min, out Vector3 max)
        {
            switch (Type)
            {
                case ShapeType.Sphere:
                    Vector3 extentsSphere = Vector3.one * Radius;

                    min = position - extentsSphere;
                    max = position + extentsSphere;
                    break;

                case ShapeType.Box:

                    Vector3 right = rotation * Vector3.right;
                    Vector3 up = rotation * Vector3.up;
                    Vector3 forward = rotation * Vector3.forward;

                    Vector3 worldExtents = new Vector3(
                        Mathf.Abs(right.x) * HalfExtents.x + Mathf.Abs(up.x) * HalfExtents.y + Mathf.Abs(forward.x) + HalfExtents.z,

                        Mathf.Abs(right.y) * HalfExtents.x + Mathf.Abs(up.y) * HalfExtents.y + Mathf.Abs(forward.y) * HalfExtents.z,

                        Mathf.Abs(right.z) * HalfExtents.x + Mathf.Abs(up.z) * HalfExtents.y + Mathf.Abs(forward.z) * HalfExtents.z);

                    min = position - worldExtents;
                    max = position + worldExtents;
                    break;

                //case ShapeType.Box:
                //    min = position - HalfExtents;
                //    max = position + HalfExtents;
                //    break;

                case ShapeType.Capsule:

                    GetCapsulePoints(position, rotation, out Vector3 a, out Vector3 b);

                    Vector3 radiusExtents = Radius * Vector3.one;

                    min = Vector3.Min(a, b) - radiusExtents;
                    max = Vector3.Max(a, b) + radiusExtents;
                    break;

                default:
                    min = position;
                    max = position;
                    break;
            }
        }

        public void GetCapsulePoints(Vector3 position, Quaternion rotation, out Vector3 pointA, out Vector3 pointB)
        {
            Vector3 up = rotation * Vector3.up;

            float segmentHalfLength = Mathf.Max(0f, Height * 0.5f - Radius);

            pointA = position + up * segmentHalfLength;
            pointB = position - up * segmentHalfLength;
        }
    }
}