using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using UnityEngine;

public class EnemyEntity : CombatEntity, ICollisionHandler
{
    public CollisionObject CollisionObject { get; private set; }

    [SerializeField] private float ColliderScale;

    protected override void Awake()
    {
        base.Awake();

        Team = Team.Enemy;

        CollisionObject = new CollisionObject()
        {
            Entity = this,
            Active = true,
            Position = CachedTransform.position,
            CollisionHandler = this,
            CollisionShape = new CollisionShape(),
            Layer = CollisionLayer.Enemy
        };

        CollisionObject.CollisionShape.Type = ShapeType.Box;
        CollisionObject.CollisionShape.HalfExtents = (ColliderScale * 0.5f) * Vector3.one;
    }

    private void Start()
    {
        GameManager.RegisterCollisionObject(CollisionObject);
    }

    private void Update()
    {
        CollisionObject.Position = CachedTransform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, ColliderScale * Vector3.one);
    }

    public void OnCollision(CollisionObject other)
    {
        
    }
}
