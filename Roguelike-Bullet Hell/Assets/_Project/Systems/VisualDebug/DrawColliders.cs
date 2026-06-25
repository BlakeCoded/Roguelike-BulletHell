using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VisualDebugging
{
    public static class DrawColliders
    {
        private static void DrawLine(Vector3 a, Vector3 b, Color c, float duration)
        {
            Debug.DrawLine(a, b, c, duration);
        }

        public static void DrawOBB(Vector3 center, Quaternion rotation, Vector3 halfExtents, Color color, float duration = 0f)
        {
            Vector3[] corners = new Vector3[8];

            // Local space corners
            Vector3 hx = new Vector3(halfExtents.x, 0, 0);
            Vector3 hy = new Vector3(0, halfExtents.y, 0);
            Vector3 hz = new Vector3(0, 0, halfExtents.z);

            corners[0] = center + rotation * (hx + hy + hz);
            corners[1] = center + rotation * (hx + hy - hz);
            corners[2] = center + rotation * (hx - hy + hz);
            corners[3] = center + rotation * (hx - hy - hz);

            corners[4] = center + rotation * (-hx + hy + hz);
            corners[5] = center + rotation * (-hx + hy - hz);
            corners[6] = center + rotation * (-hx - hy + hz);
            corners[7] = center + rotation * (-hx - hy - hz);

            // Bottom square
            DrawLine(corners[0], corners[1], color, duration);
            DrawLine(corners[1], corners[3], color, duration);
            DrawLine(corners[3], corners[2], color, duration);
            DrawLine(corners[2], corners[0], color, duration);

            // Top square
            DrawLine(corners[4], corners[5], color, duration);
            DrawLine(corners[5], corners[7], color, duration);
            DrawLine(corners[7], corners[6], color, duration);
            DrawLine(corners[6], corners[4], color, duration);

            // Vertical edges
            DrawLine(corners[0], corners[4], color, duration);
            DrawLine(corners[1], corners[5], color, duration);
            DrawLine(corners[2], corners[6], color, duration);
            DrawLine(corners[3], corners[7], color, duration);
        }

        public static void DrawCapsuel(Vector3 position, Quaternion rotation, float radius, float height)
        {
            float segmentHalfLength = Mathf.Max(0f, height * 0.5f - radius);

            Vector3 up = rotation * Vector3.up;
            Vector3 right = rotation * Vector3.right;
            Vector3 forward = rotation * Vector3.forward;

            Vector3 top = position + up * segmentHalfLength;
            Vector3 bottom = position - up * segmentHalfLength;

            // End spheres
            Gizmos.DrawWireSphere(top, radius);
            Gizmos.DrawWireSphere(bottom, radius);

            // Connect spheres
            Gizmos.DrawLine(top + right * radius, bottom + right * radius);
            Gizmos.DrawLine(top - right * radius, bottom - right * radius);
            Gizmos.DrawLine(top + forward * radius, bottom + forward * radius);
            Gizmos.DrawLine(top - forward * radius, bottom - forward * radius);
        }

        public static void DrawSphere(Vector3 position, float radius)
        {
            Gizmos.DrawWireSphere(position, radius);
        }
    }
}