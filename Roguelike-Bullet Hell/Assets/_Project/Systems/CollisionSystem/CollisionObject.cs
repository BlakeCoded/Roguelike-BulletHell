using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Collision
{
    [System.Serializable]
    public class CollisionObject
    {
        // CONFIGURATION
        public CollisionLayer Layer;
        public CollisionShape CollisionShape;

        // OWNER
        [HideInInspector] public CombatEntity Entity;
        [HideInInspector] public ICollisionHandler CollisionHandler;

        // TRANSFORM
        [HideInInspector] public Vector3 Position;
        [HideInInspector] public Quaternion Rotation;

        // RUNTIME STATE
        [HideInInspector] public bool Active = true;
        [HideInInspector] public bool PendingCollision;
        [HideInInspector] public int LastRayCastId;

        // SPATIAL HASH
        [HideInInspector] public readonly List<Vector3Int> OccupiedCells = new();
        [HideInInspector] public Vector3Int MinCell;
        [HideInInspector] public Vector3Int MaxCell;
    }

    [System.Serializable]
    public enum CollisionLayer
    {
        Player,
        PlayerProjectiles,
        Enemy,
        EnemyProjectiles,
        World
    }
}