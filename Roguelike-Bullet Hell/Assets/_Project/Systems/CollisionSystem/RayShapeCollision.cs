using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public static class RayShapeCollision
    {
        public static bool Test(Vector3 origin, Vector3 direction, float maxDistance, CollisionShape shape, Vector3 position, out float hitDistance)
        {
            switch(shape.Type)
            {
                case ShapeType.Sphere:
                    return RaySphere(origin, direction, maxDistance, position, shape.Radius,  out hitDistance);

                case ShapeType.Box:
                    return RayBox(origin, direction, maxDistance, position, shape.HalfExtents, out hitDistance);

                case ShapeType.Capsule:
                    hitDistance = 0;
                    return false;

                default:
                    hitDistance = 0;
                    return false;
            }
        }

        private static bool RaySphere(Vector3 origin, Vector3 direction, float maxDistance, Vector3 sphereCenter, float radius, out float hitDistance)
        {
            hitDistance = 0f;

            Vector3 toSphere = sphereCenter - origin;

            float projection = Vector3.Dot(toSphere, direction);

            if(projection < 0) return false;

            Vector3 closestPoint = origin + direction * projection;

            float sqrDistanceToCenter = (sphereCenter - closestPoint).sqrMagnitude;

            float radiusSqr = radius * radius;

            if(sqrDistanceToCenter > radiusSqr) return false;

            float offset = Mathf.Sqrt(radiusSqr -  sqrDistanceToCenter);

            hitDistance = projection - offset;

            if(hitDistance > maxDistance) return false;

            return true;
        }

        private static bool RayBox(Vector3 origin, Vector3 direction, float maxDistance, Vector3 boxCenter, Vector3 halfExtents, out float hitDistance)
        {
            hitDistance = 0f;

            Vector3 min = boxCenter - halfExtents;
            Vector3 max = boxCenter + halfExtents;

            float tMin = float.NegativeInfinity;
            float tMax = float.PositiveInfinity;

            // X
            if(Mathf.Abs(direction.x) < 0.0001f)
            {
                if (origin.x < min.x || origin.x > max.x) return false;
            }
            else
            {
                float tx1 = (min.x - origin.x) / direction.x;
                float tx2 = (max.x - origin.x) / direction.x;

                tMin = Mathf.Max(tMin, Mathf.Min(tx1, tx2));
                tMax = Mathf.Min(tMax, Mathf.Max(tx1, tx2));
            }

            // Y
            if (Mathf.Abs(direction.y) < 0.0001f)
            {
                if (origin.y < min.y || origin.y > max.y)
                    return false;
            }
            else
            {
                float ty1 = (min.y - origin.y) / direction.y;
                float ty2 = (max.y - origin.y) / direction.y;

                tMin = Mathf.Max(tMin, Mathf.Min(ty1, ty2));
                tMax = Mathf.Min(tMax, Mathf.Max(ty1, ty2));
            }

            // Z
            if (Mathf.Abs(direction.z) < 0.0001f)
            {
                if (origin.z < min.z || origin.z > max.z)
                    return false;
            }
            else
            {
                float tz1 = (min.z - origin.z) / direction.z;
                float tz2 = (max.z - origin.z) / direction.z;

                tMin = Mathf.Max(tMin, Mathf.Min(tz1, tz2));
                tMax = Mathf.Min(tMax, Mathf.Max(tz1, tz2));
            }

            if (tMax < tMin) return false;

            hitDistance = tMin < 0 ? tMax : tMin;

            if (hitDistance < 0f || hitDistance > maxDistance) return false;

            return true;
        }
    }
}