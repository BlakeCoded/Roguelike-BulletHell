using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public static class RayShapeCollision
    {
        public static bool Test(Vector3 origin, Vector3 direction, float maxDistance, Vector3 position, Quaternion rotation, CollisionShape shape, out float hitDistance)
        {
            switch(shape.Type)
            {
                case ShapeType.Sphere:
                    return RaySphere(origin, direction, maxDistance, position, shape.Radius,  out hitDistance);

                case ShapeType.Box:
                    return RayBox(origin, direction, maxDistance, position, rotation, shape.HalfExtents, out hitDistance);

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

        private static bool RayBox(Vector3 origin, Vector3 direction, float maxDistance, Vector3 boxCenter, Quaternion rotation, Vector3 halfExtents, out float hitDistance)
        {
            hitDistance = 0f;

            Vector3 localOrigin = Quaternion.Inverse(rotation) * (origin - boxCenter);
            Vector3 localDir = Quaternion.Inverse(rotation) * direction;

            localDir = localDir.normalized;

            Vector3 min = -halfExtents;
            Vector3 max = halfExtents;

            float tMin = float.NegativeInfinity;
            float tMax = float.PositiveInfinity;

            // X
            if(Mathf.Abs(localDir.x) < 0.0001f)
            {
                if (localOrigin.x < min.x || localOrigin.x > max.x) return false;
            }
            else
            {
                float tx1 = (min.x - localOrigin.x) / localDir.x;
                float tx2 = (max.x - localOrigin.x) / localDir.x;

                tMin = Mathf.Max(tMin, Mathf.Min(tx1, tx2));
                tMax = Mathf.Min(tMax, Mathf.Max(tx1, tx2));
            }

            // Y
            if (Mathf.Abs(localDir.y) < 0.0001f)
            {
                if (localOrigin.y < min.y || localOrigin.y > max.y) return false;
            }
            else
            {
                float ty1 = (min.y - localOrigin.y) / localDir.y;
                float ty2 = (max.y - localOrigin.y) / localDir.y;

                tMin = Mathf.Max(tMin, Mathf.Min(ty1, ty2));
                tMax = Mathf.Min(tMax, Mathf.Max(ty1, ty2));
            }

            // Z
            if (Mathf.Abs(localDir.z) < 0.0001f)
            {
                if (localOrigin.z < min.z || localOrigin.z > max.z) return false;
            }
            else
            {
                float tz1 = (min.z - localOrigin.z) / localDir.z;
                float tz2 = (max.z - localOrigin.z) / localDir.z;

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