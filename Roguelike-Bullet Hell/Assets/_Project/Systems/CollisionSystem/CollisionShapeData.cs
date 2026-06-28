using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    [System.Serializable]
    public struct CollisionShapeData
    {
        public ShapeType Type;

        public float Radius;
        public Vector3 HalfExtents;
        public float Height;

        public static CollisionShapeData Default => new CollisionShapeData
        {
            Type = ShapeType.Sphere,
            Radius = 0.5f,
            HalfExtents = 0.5f * Vector3.one,
            Height = 2f
        };

        public CollisionShape Build(Transform t)
        {
            Vector3 scale = t.lossyScale;

            switch (this.Type)
            {
                case ShapeType.Sphere:
                    return new CollisionShape
                    {
                        Type = this.Type,
                        Radius = Radius * Mathf.Max(scale.x, scale.y, scale.z)
                    };

                case ShapeType.Box:
                    return new CollisionShape
                    {
                        Type = this.Type,
                        HalfExtents = Vector3.Scale(HalfExtents, scale)
                    };

                case ShapeType.Capsule:
                    float radius = Radius * Mathf.Max(scale.x, scale.z);

                    return new CollisionShape
                    {
                        Type = this.Type,
                        Radius = radius,
                        Height = Mathf.Max(Height * scale.y, radius * 2f)
                    };

                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), Type, "Unsupported collision shape.");
            }
        }
    }
}