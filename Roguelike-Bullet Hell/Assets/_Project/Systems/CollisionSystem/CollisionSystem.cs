using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Raycast
        private int currentRaycastId = int.MinValue;
        private readonly List<RaycastHitData> raycastHits = new();

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

        public void Enable(CollisionObject obj)
        {
            obj.Active = true;
            grid.Add(obj);
        }

        public void Disable(CollisionObject obj)
        {
            obj.Active = false;
            grid.Remove(obj);
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
            foreach(CollisionObject obj in collisionObjects)
            {
                grid.UpdateObject(obj);
            }
        }

        private void ProcessRemovals()
        {
            foreach(CollisionObject obj in pendingRemovals)
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
                collision.A.CollisionHandler?.OnHit(collision.B);
            }

            collisionEvents.Clear();

            foreach (var obj in collisionObjects)
            {
                obj.PendingCollision = false;
            }
        }

        private void CheckCollisions()
        {
            foreach(CollisionObject obj in collisionObjects)
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
                if(!other.Active) continue;

                if(!CollisionMatrix.CanCollide(obj.Layer, other.Layer)) continue;

                if(ShapeCollision.Test(obj.CollisionShape, obj.Position, obj.Rotation, other.CollisionShape, other.Position, other.Rotation))
                {
                    collisionEvents.Add(new CollisionEvent(obj, other));
                    obj.PendingCollision = true;
                }

                if(obj.PendingCollision) break;
            }
        }

        public bool Raycast(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out RaycastHitData hit)
        {
            hit = default;
            currentRaycastId++;

            bool hitFound = false;
            float closestDistance = distance;
            CollisionObject closestObject = null;

            GridRayTraversal traversal = new GridRayTraversal(origin, direction, grid.CELLSIZE);

            while (traversal.DistanceTravelled < closestDistance)
            {
                grid.GetObjectsFromCell(traversal.CurrentCell, results);

                foreach (CollisionObject other in results)
                {
                    if(other.LastRayCastId == currentRaycastId) continue;

                    other.LastRayCastId = currentRaycastId;

                    if (!other.Active) continue;

                    if (!CollisionMatrix.CanCollide(collisionLayer, other.Layer)) continue;

                    if (RayShapeCollision.Test(origin, direction, closestDistance, other.Position, other.Rotation, other.CollisionShape, out float hitDistance))
                    {
                        if (hitDistance < closestDistance)
                        {
                            closestObject = other;
                            closestDistance = hitDistance;
                        }

                        hitFound = true;
                    }
                }

                traversal.Step();
            }

            if(hitFound)
            {
                hit = new RaycastHitData
                {
                    CollisionObject = closestObject,
                    HitDistance = closestDistance,
                    HitPoint = origin + direction * closestDistance
                };
            }

            return hitFound;
        }

        public bool RaycastAll(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out List<RaycastHitData> hits)
        {
            raycastHits.Clear();

            currentRaycastId++;

            bool foundHit = false;

            grid.GetAllObjectsAlongRay(origin, direction, distance, results);

            foreach (CollisionObject other in results)
            {
                if (other.LastRayCastId == currentRaycastId) continue;

                other.LastRayCastId = currentRaycastId;

                if (!other.Active) continue;

                if (!CollisionMatrix.CanCollide(collisionLayer, other.Layer)) continue;

                if (RayShapeCollision.Test(origin, direction, distance, other.Position, other.Rotation, other.CollisionShape, out float hitDistance))
                {
                    raycastHits.Add(new RaycastHitData
                    {
                        CollisionObject = other,
                        HitDistance = hitDistance,
                        HitPoint = origin + direction * hitDistance
                    });

                    foundHit = true;
                }
            }

            hits = raycastHits;

            return foundHit;
        }
    }
}