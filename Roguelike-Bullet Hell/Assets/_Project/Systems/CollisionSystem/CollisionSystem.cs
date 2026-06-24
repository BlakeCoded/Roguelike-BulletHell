using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Combat;
using System.Transactions;

namespace Collision
{
    public class CollisionSystem
    {
        private readonly SpatialGrid grid;
        private readonly List<CollisionObject> results = new();
        private readonly List<CollisionObject> collisionObjects = new();
        private readonly List<CollisionObject> pendingAdditions = new();
        private readonly List<CollisionObject> pendingRemovals = new();
        private readonly List<CollisionEvent> collisionEvents = new();

        public CollisionSystem(float cellSize)
        {
            this.grid = new SpatialGrid(cellSize);
        }

        public void Register(CollisionObject obj)
        {
            pendingAdditions.Add(obj);
        }

        public void Unregister(CollisionObject obj)
        {
            pendingRemovals.Add(obj);
        }

        public void Tick()
        {
            UpdateObjects();
            CheckCollisions();
            ProcessRemovals();
            ProcessAdditions();
            ProcessCollisions();
        }

        private void UpdateObjects()
        {
            foreach (CollisionObject obj in collisionObjects)
            {
                grid.UpdateObject(obj);
            }
        }

        private void ProcessRemovals()
        {
            foreach (CollisionObject obj in pendingRemovals)
            {
                collisionObjects.Remove(obj);
                grid.Remove(obj);
            }

            pendingRemovals.Clear();
        }

        private void ProcessAdditions()
        {
            foreach(CollisionObject obj in pendingAdditions)
            {
                collisionObjects.Add(obj);
                grid.Add(obj);
            }

            pendingAdditions.Clear();
        }

        private void ProcessCollisions()
        {
            foreach(CollisionEvent collision in collisionEvents)
            {
                collision.A.CollisionHandler?.OnCollision(collision.B);
            }

            collisionEvents.Clear();

            foreach (var obj in collisionObjects)
            {
                obj.PendingCollision = false;
            }
        }

        private void CheckCollisions()
        {
            foreach (CollisionObject obj in collisionObjects)
            {
                CheckObject(obj);
            }
        }

        private void CheckObject(CollisionObject obj)
        {
            if(!obj.Active) return;

            grid.GetNearby(obj, results);

            foreach(CollisionObject other in results)
            {
                if (!other.Active) continue;

                // Filter
                if(!CollisionMatrix.CanCollide(obj.Layer, other.Layer)) continue;

                // Shape Check
                if(ShapeCollision.Test(obj.CollisionShape, obj.Position, other.CollisionShape, other.Position))
                {
                    collisionEvents.Add(new CollisionEvent(obj, other));
                    obj.PendingCollision = true;
                }

                if (obj.PendingCollision) break;
            }
        }

        public bool Raycast(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out RaycastHitData hit)
        {
            hit = default;

            bool foundHit = false;
            float closestDistance = distance;

            grid.GetObjectsAlongRay(origin, direction, distance, results);

            foreach (CollisionObject other in results)
            {
                if (!other.Active)
                    continue;

                // Filter
                if (!CollisionMatrix.CanCollide(collisionLayer, other.Layer)) continue;

                if (RayShapeCollision.Test(
                    origin,
                    direction,
                    distance,
                    other.CollisionShape,
                    other.Position,
                    out float hitDistance))
                {
                    if (hitDistance < closestDistance)
                    {
                        closestDistance = hitDistance;

                        hit = new RaycastHitData
                        {
                            CollisionObject = other,
                            HitDistance = hitDistance,
                            HitPoint = origin + direction * hitDistance
                        };

                        foundHit = true;
                    }
                }
            }

            //foreach (CollisionObject other in collisionObjects)
            //{
            //    if (!other.Active)
            //        continue;

            //    if (RayShapeCollision.Test(
            //        origin,
            //        direction,
            //        distance,
            //        other.CollisionShape,
            //        other.Position,
            //        out float hitDistance))
            //    {
            //        if (hitDistance < closestDistance)
            //        {
            //            closestDistance = hitDistance;

            //            hit = new RaycastHitData
            //            {
            //                CollisionObject = other,
            //                HitDistance = hitDistance,
            //                HitPoint = origin + direction * hitDistance
            //            };

            //            foundHit = true;
            //        }
            //    }
            //}

            return foundHit;
        }
    }
}