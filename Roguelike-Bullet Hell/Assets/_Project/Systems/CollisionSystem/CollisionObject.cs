using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Collision
{
    [System.Serializable]
    public class CollisionObject
    {
        [HideInInspector] public CombatEntity Entity;

        [HideInInspector] public bool Active = true;

        [HideInInspector] public Vector3 Position;
        [HideInInspector] public Quaternion Rotation;

        public CollisionLayer Layer;

        public CollisionShape CollisionShape;

        [HideInInspector] public ICollisionHandler CollisionHandler;

        [HideInInspector] public readonly List<Vector3Int> OccupiedCells = new();
        [HideInInspector] public Vector3Int MinCell;
        [HideInInspector] public Vector3Int MaxCell;

        [HideInInspector] public bool PendingCollision;
        [HideInInspector] public int LastRayCastId;
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