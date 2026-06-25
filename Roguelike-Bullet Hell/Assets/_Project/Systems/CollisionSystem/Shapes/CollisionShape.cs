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

        public void GetBounds(Vector3 position, out Vector3 min, out Vector3 max)
        {
            switch (Type)
            {
                case ShapeType.Sphere:
                    Vector3 extents = Vector3.one * Radius;

                    min = position - extents;
                    max = position + extents;
                    break;

                case ShapeType.Box:
                    min = position - HalfExtents;
                    max = position + HalfExtents;
                    break;

                case ShapeType.Capsule:
                    //float halfHeight = Height * 0.5f;

                    //Vector3 extents = new Vector3(
                    //    Radius,
                    //    halfHeight + Radius,
                    //    Radius);

                    //min = position - extents;
                    //max = position + extents;
                    min = position;
                    max = position;
                    break;

                default:
                    min = position;
                    max = position;
                    break;
            }
        }
    }
}