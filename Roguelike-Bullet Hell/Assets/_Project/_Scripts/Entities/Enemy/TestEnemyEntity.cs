using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using UnityEngine;
using VisualDebugging;

public class TestEnemyEntity : CombatEntity, ICollisionHandler
{
    public CollisionObject CollisionObject => collisionObject;
    [SerializeField] private CollisionObject collisionObject;

    protected override void Awake()
    {
        base.Awake();

        Team = Team.Enemy;

        InitializeCollisionObject();
    }

    private void Start()
    {
        GameManager.RegisterCollisionObject(collisionObject);
    }

    private void Update()
    {
        SyncCollisionTransform();

        //DrawCollider();
    }

    private void InitializeCollisionObject()
    {
        collisionObject.Entity = this;
        collisionObject.CollisionHandler = this;
        collisionObject.Active = true;

        SyncCollisionTransform();
    }
    
    private void SyncCollisionTransform()
    {
        collisionObject.Position = CachedTransform.position;
        collisionObject.Rotation = CachedTransform.rotation;
    }

    private void DrawCollider()
    {
        switch(collisionObject.CollisionShape.Type)
        {
            case ShapeType.Sphere:
                DrawColliders.DrawSphere(transform.position, collisionObject.CollisionShape.Radius);
                break;

            case ShapeType.Box:
                DrawColliders.DrawOBB(transform.position, transform.rotation, collisionObject.CollisionShape.HalfExtents, Color.white);
                break;

            case ShapeType.Capsule:
                DrawColliders.DrawCapsuel(transform.position, transform.rotation, collisionObject.CollisionShape.Radius, collisionObject.CollisionShape.Height);
                break;
        }
    }

    public void OnCollision(CollisionObject other)
    {
        
    }

    private void OnDrawGizmos()
    {
        DrawCollider();
    }
}
