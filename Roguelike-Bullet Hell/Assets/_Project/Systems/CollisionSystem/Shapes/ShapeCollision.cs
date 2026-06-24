using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public static class ShapeCollision
    {
        public static bool Test(CollisionShape aShape, Vector3 aPos, CollisionShape bShape, Vector3 bPos)
        {
            if (aShape.Type == ShapeType.Sphere && bShape.Type == ShapeType.Sphere)
                return SphereSphere(aPos, aShape.Radius, bPos, bShape.Radius);

            if (aShape.Type == ShapeType.Sphere && bShape.Type == ShapeType.Box)
                return SphereBox(aPos, aShape.Radius, bPos, bShape.HalfExtents);

            if (aShape.Type == ShapeType.Box && bShape.Type == ShapeType.Sphere)
                return SphereBox(bPos, bShape.Radius, aPos, aShape.HalfExtents);

            if (aShape.Type == ShapeType.Box && bShape.Type == ShapeType.Box)
                return BoxBox(aPos, aShape.HalfExtents, bPos, bShape.HalfExtents);

            return false;
        }

        private static bool SphereSphere(Vector3 a, float r1, Vector3 b, float r2)
        {
            float r = r1 + r2;
            return (a - b).sqrMagnitude <= r * r;
        }

        private static bool BoxBox(Vector3 a, Vector3 e1, Vector3 b, Vector3 e2)
        {
            return Mathf.Abs(a.x - b.x) <= (e1.x + e2.x) &&
               Mathf.Abs(a.y - b.y) <= (e1.y + e2.y) &&
               Mathf.Abs(a.z - b.z) <= (e1.z + e2.z);
        }

        private static bool SphereBox(Vector3 spherePos, float radius, Vector3 boxPos, Vector3 half)
        {
            Vector3 Closest = new Vector3(
                Mathf.Clamp(spherePos.x, boxPos.x - half.x, boxPos.x + half.x),
                Mathf.Clamp(spherePos.y, boxPos.y - half.y, boxPos.y + half.y),
                Mathf.Clamp(spherePos.z, boxPos.z - half.z, boxPos.z + half.z));

            return (Closest - spherePos).sqrMagnitude <= radius * radius;
        }
    }
}