using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Collision
{
    public class CollisionObject
    {
        public CombatEntity Entity;

        public bool Active = true;

        public Vector3 Position;

        public CollisionLayer Layer;

        public CollisionShape CollisionShape;

        public ICollisionHandler CollisionHandler;

        public readonly List<Vector3Int> OccupiedCells = new();
        public Vector3Int MinCell;
        public Vector3Int MaxCell;

        public bool PendingCollision;
    }

    public enum CollisionLayer
    {
        Player,
        PlayerProjectiles,
        Enemy,
        EnemyProjectiles,
        World
    }
}