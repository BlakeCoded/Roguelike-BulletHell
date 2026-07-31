using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using UnityEngine.UIElements;
using VisualDebugging;

namespace Collision
{
    public class CollisionObjectComponent : MonoBehaviour
    {
        // CONFIGURATION
        public CollisionLayer Layer;
        public CollisionShapeData ShapeData = CollisionShapeData.Default;

        public CollisionObject BuildCollisionObject(CombatEntity entity, ICollisionHandler collisionHandler)
        {
            Transform t = transform;

            return new CollisionObject
            {
                Active = true,
                Layer = Layer,
                CollisionShape = ShapeData.Build(t),
                Entity = entity,
                CollisionHandler = collisionHandler,
                Transform = t,
                Position = t.position,
                Rotation = t.rotation,
            };
        }

        private void OnDrawGizmos()
        {
            DrawCollider();
        }

        private void DrawCollider()
        {
            switch (ShapeData.Type)
            {
                case ShapeType.Sphere:
                    DrawColliders.DrawSphere(transform.position, ShapeData.Radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z));
                    break;

                case ShapeType.Box:
                    DrawColliders.DrawOBB(transform.position, transform.rotation, Vector3.Scale(ShapeData.HalfExtents, transform.lossyScale), Color.white);
                    break;

                case ShapeType.Capsule:
                    float radius = ShapeData.Radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.z);
                    float totalHeight = Mathf.Max(ShapeData.Height * transform.lossyScale.y, radius * 2f);

                    DrawColliders.DrawCapsuel(transform.position, transform.rotation, radius, totalHeight);
                    break;
            }
        }
    }
}