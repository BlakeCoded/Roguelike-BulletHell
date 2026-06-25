using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public static class ShapeCollision
    {
        public static bool Test(CollisionShape aShape, Vector3 aPos, Quaternion aRot, CollisionShape bShape, Vector3 bPos, Quaternion bRot)
        {
            if (aShape.Type == ShapeType.Sphere && bShape.Type == ShapeType.Sphere)
                return SphereSphere(aPos, aShape.Radius, bPos, bShape.Radius);

            if (aShape.Type == ShapeType.Sphere && bShape.Type == ShapeType.Box)
                return SphereBox(aPos, aShape.Radius, bPos, bShape.HalfExtents, bRot);

            if(aShape.Type == ShapeType.Sphere && bShape.Type == ShapeType.Capsule)
            {
                bShape.GetCapsulePoints(bPos, bRot, out Vector3 capsuleA, out Vector3 capsuleB);
                return SphereCapsule(aPos, aShape.Radius, capsuleA, capsuleB, bShape.Radius);
            }  

            if (aShape.Type == ShapeType.Box && bShape.Type == ShapeType.Sphere)
                return SphereBox(bPos, bShape.Radius, aPos, aShape.HalfExtents, aRot);

            if (aShape.Type == ShapeType.Box && bShape.Type == ShapeType.Box)
                return AABBBoxBox(aPos, aShape.HalfExtents, bPos, bShape.HalfExtents);

            if (aShape.Type == ShapeType.Box && bShape.Type == ShapeType.Capsule)
            {
                bShape.GetCapsulePoints(bPos, bRot, out Vector3 pointA, out Vector3 pointB);
                return BoxCapsule(aPos, aRot, aShape.HalfExtents, pointA, pointB, bShape.Radius);
            }  

            if (aShape.Type == ShapeType.Capsule && bShape.Type == ShapeType.Capsule)
            {
                aShape.GetCapsulePoints(aPos, aRot, out Vector3 a0, out Vector3 a1);
                bShape.GetCapsulePoints(bPos, bRot, out Vector3 b0, out Vector3 b1);
                return CapsuleCapsule(a0, a1, aShape.Radius, b0, b1, bShape.Radius);
            }

            if (aShape.Type == ShapeType.Capsule && bShape.Type == ShapeType.Sphere)
            {
                aShape.GetCapsulePoints(aPos, aRot, out Vector3 capsuleA, out Vector3 capsuleB);
                return SphereCapsule(bPos, bShape.Radius, capsuleA, capsuleB, aShape.Radius);
            }

            if (aShape.Type == ShapeType.Capsule && bShape.Type == ShapeType.Box)
            {
                aShape.GetCapsulePoints(aPos, aRot, out Vector3 pointA, out Vector3 pointB);
                return BoxCapsule(bPos, bRot, bShape.HalfExtents, pointA, pointB, aShape.Radius);
            }

                return false;
        }

        private static bool SphereSphere(Vector3 a, float r1, Vector3 b, float r2)
        {
            float r = r1 + r2;
            return (a - b).sqrMagnitude <= r * r;
        }

        private static bool AABBBoxBox(Vector3 aPos, Vector3 aHalf, Vector3 bPos, Vector3 bHalf)
        {
            return Mathf.Abs(aPos.x - bPos.x) <= (aHalf.x + bHalf.x) &&
                   Mathf.Abs(aPos.y - bPos.y) <= (aHalf.y + bHalf.y) &&
                   Mathf.Abs(aPos.z - bPos.z) <= (aHalf.z + bHalf.z);
        }

        private static bool BoxBox(Vector3 aPos, Quaternion aRot, Vector3 aHalf, Vector3 bPos, Quaternion bRot,Vector3 bHalf)
        {
            Vector3[] A =
            {
                aRot * Vector3.right,
                aRot * Vector3.up,
                aRot * Vector3.forward
            };

            Vector3[] B =
            {
                bRot * Vector3.right,
                bRot * Vector3.up,
                bRot * Vector3.forward
            };

            float[,] R = new float[3, 3];
            float[,] AbsR = new float[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    R[i, j] = Vector3.Dot(A[i], B[j]);
                    AbsR[i, j] = Mathf.Abs(R[i, j]) + 0.0001f;
                }
            }

            Vector3 tWorld = bPos - aPos;

            Vector3 t = new Vector3(
                Vector3.Dot(tWorld, A[0]),
                Vector3.Dot(tWorld, A[1]),
                Vector3.Dot(tWorld, A[2]));

            float ra, rb;

            for (int i = 0; i < 3; i++)
            {
                ra = aHalf[i];

                rb =
                    bHalf.x * AbsR[i, 0] +
                    bHalf.y * AbsR[i, 1] +
                    bHalf.z * AbsR[i, 2];

                if (Mathf.Abs(t[i]) > ra + rb)
                    return false;
            }

            for (int i = 0; i < 3; i++)
            {
                ra =
                    aHalf.x * AbsR[0, i] +
                    aHalf.y * AbsR[1, i] +
                    aHalf.z * AbsR[2, i];

                rb = bHalf[i];

                if (Mathf.Abs(
                    t.x * R[0, i] +
                    t.y * R[1, i] +
                    t.z * R[2, i])
                    > ra + rb)
                    return false;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ra =
                        aHalf[(i + 1) % 3] * AbsR[(i + 2) % 3, j] +
                        aHalf[(i + 2) % 3] * AbsR[(i + 1) % 3, j];

                    rb =
                        bHalf[(j + 1) % 3] * AbsR[i, (j + 2) % 3] +
                        bHalf[(j + 2) % 3] * AbsR[i, (j + 1) % 3];

                    float distance =
                        Mathf.Abs(
                            t[(i + 2) % 3] * R[(i + 1) % 3, j] -
                            t[(i + 1) % 3] * R[(i + 2) % 3, j]);

                    if (distance > ra + rb)
                        return false;
                }
            }

            return true;
        }

        private static bool CapsuleCapsule(Vector3 a0, Vector3 a1, float radiusA, Vector3 b0, Vector3 b1, float radiusB)
        {
            float distanceSqr = SegmentSegmentDistanceSquared(a0, a1, b0, b1);

            float combinedRadius = radiusA + radiusB;

            return distanceSqr <= combinedRadius * combinedRadius;
        }

        private static bool SphereBox(Vector3 spherePos, float radius, Vector3 boxPos, Vector3 half, Quaternion boxRot)
        {
            Vector3 localSphere = Quaternion.Inverse(boxRot) * (spherePos - boxPos);

            Vector3 Closest = new Vector3(
                Mathf.Clamp(localSphere.x, -half.x, half.x),
                Mathf.Clamp(localSphere.y, -half.y, half.y),
                Mathf.Clamp(localSphere.z, -half.z, half.z));

            return (localSphere - Closest).sqrMagnitude <= radius * radius;
        }

        private static bool SphereCapsule(Vector3 spherePos, float sphereRadius, Vector3 capsuleA, Vector3 capsuleB, float capsuleRadius)
        {
            Vector3 closest = ClosestPointOnSegment(spherePos, capsuleA, capsuleB);

            float combinedRadius = sphereRadius + capsuleRadius;

            return (spherePos - closest).sqrMagnitude <= combinedRadius * combinedRadius;
        }

        private static bool BoxCapsule(Vector3 boxPos, Quaternion boxRot, Vector3 halfExtents, Vector3 capsuleA, Vector3 capsuleB, float capsuleRadius)
        {
            const int samples = 5;

            for (int i = 0; i <= samples; i++)
            {
                float t = i / (float)samples;

                Vector3 point = Vector3.Lerp(capsuleA, capsuleB, t);

                Vector3 closest = ClosestPointOnBox(point, boxPos, boxRot, halfExtents);

                if ((point - closest).sqrMagnitude <= capsuleRadius * capsuleRadius) return true;
            }

            return false;
        }

        private static Vector3 ClosestPointOnBox(Vector3 point,Vector3 boxPos,Quaternion boxRot, Vector3 halfExtents)
        {
            Vector3 localPoint = Quaternion.Inverse(boxRot) * (point - boxPos);

            Vector3 localClosest = new Vector3(
                Mathf.Clamp(localPoint.x, -halfExtents.x, halfExtents.x),
                Mathf.Clamp(localPoint.y, -halfExtents.y, halfExtents.y),
                Mathf.Clamp(localPoint.z, -halfExtents.z, halfExtents.z));

            return boxPos + boxRot * localClosest;
        }

        private static Vector3 ClosestPointOnSegment(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd)
        {
            Vector3 segment = segmentEnd - segmentStart;

            float lengthSqr = segment.sqrMagnitude;

            if (lengthSqr <= Mathf.Epsilon)
                return segmentStart;

            float t = Vector3.Dot(point - segmentStart, segment) / lengthSqr;
            t = Mathf.Clamp01(t);

            return segmentStart + segment * t;
        }

        private static float SegmentSegmentDistanceSquared(Vector3 p1, Vector3 q1, Vector3 p2, Vector3 q2)
        {
            Vector3 d1 = q1 - p1;
            Vector3 d2 = q2 - p2;
            Vector3 r = p1 - p2;

            float a = Vector3.Dot(d1, d1);
            float e = Vector3.Dot(d2, d2);
            float f = Vector3.Dot(d2, r);

            float s;
            float t;

            if (a <= Mathf.Epsilon && e <= Mathf.Epsilon)
                return (p1 - p2).sqrMagnitude;

            if (a <= Mathf.Epsilon)
            {
                s = 0f;
                t = Mathf.Clamp01(f / e);
            }
            else
            {
                float c = Vector3.Dot(d1, r);

                if (e <= Mathf.Epsilon)
                {
                    t = 0f;
                    s = Mathf.Clamp01(-c / a);
                }
                else
                {
                    float b = Vector3.Dot(d1, d2);
                    float denom = a * e - b * b;

                    s = denom > Mathf.Epsilon
                        ? Mathf.Clamp01((b * f - c * e) / denom)
                        : 0f;

                    t = (b * s + f) / e;

                    if (t < 0f)
                    {
                        t = 0f;
                        s = Mathf.Clamp01(-c / a);
                    }
                    else if (t > 1f)
                    {
                        t = 1f;
                        s = Mathf.Clamp01((b - c) / a);
                    }
                }
            }

            Vector3 closestA = p1 + d1 * s;
            Vector3 closestB = p2 + d2 * t;

            return (closestA - closestB).sqrMagnitude;
        }
    }
}