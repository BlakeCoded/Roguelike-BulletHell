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
                    return RaySphere(origin, direction, maxDistance, position, shape.Radius, out hitDistance);

                case ShapeType.Box:
                    return RayBox(origin, direction, maxDistance, position, rotation, shape.HalfExtents, out hitDistance);

                case ShapeType.Capsule:
                    shape.GetCapsulePoints(position, rotation, out Vector3 pointA, out Vector3 pointB);
                    return RayCapsule(origin, direction, maxDistance, position, rotation, pointA, pointB, shape.Radius, out hitDistance);

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

        private static bool RayCapsule(Vector3 origin, Vector3 direction, float maxDistance, Vector3 capsuleCenter, Quaternion rotation, Vector3 pointA, Vector3 pointB, float radius, out float hitDistance)
        {
            hitDistance = 0f;

            float rayT;

            float distanceSqr = RaySegmentDistanceSquared(origin, direction, pointA, pointB, out rayT);

            if (rayT > maxDistance) return false;

            if (distanceSqr > radius * radius) return false;

            hitDistance = rayT;
            return true;
        }

        private static float RaySegmentDistanceSquared(Vector3 rayOrigin, Vector3 rayDirection, Vector3 segmentA, Vector3 segmentB, out float rayT)
        {
            Vector3 u = rayDirection;
            Vector3 v = segmentB - segmentA;
            Vector3 w = rayOrigin - segmentA;

            float a = Vector3.Dot(u, u);
            float b = Vector3.Dot(u, v);
            float c = Vector3.Dot(v, v);
            float d = Vector3.Dot(u, w);
            float e = Vector3.Dot(v, w);

            float denom = a * c - b * b;

            float s;
            float t;

            if (Mathf.Abs(denom) < 0.0001f)
            {
                s = 0f;
                t = Mathf.Clamp01(e / c);
            }
            else
            {
                s = (b * e - c * d) / denom;
                t = Mathf.Clamp01((a * e - b * d) / denom);

                if (s < 0f)
                    s = 0f;
            }

            Vector3 closestRay = rayOrigin + u * s;
            Vector3 closestSeg = segmentA + v * t;

            rayT = s;

            return (closestRay - closestSeg).sqrMagnitude;
        }
    }
}