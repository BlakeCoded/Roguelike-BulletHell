using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using UnityEngine;
using VisualDebugging;

public class EnemyEntity : CombatEntity, ICollisionHandler
{
    public CollisionObject CollisionObject => m_CollisionObject;
    [SerializeField] private CollisionObject m_CollisionObject;

    protected override void Awake()
    {
        base.Awake();

        Team = Team.Enemy;

        m_CollisionObject.Entity = this;
        m_CollisionObject.Active = true;
        m_CollisionObject.Position = CachedTransform.position;
        m_CollisionObject.CollisionHandler = this;
    }

    private void Start()
    {
        GameManager.RegisterCollisionObject(m_CollisionObject);
    }

    private void Update()
    {
        m_CollisionObject.Position = CachedTransform.position;
        m_CollisionObject.Rotation = CachedTransform.rotation;

        DrawColliders.DrawOBB(m_CollisionObject.Position, m_CollisionObject.Rotation, m_CollisionObject.CollisionShape.HalfExtents, Color.white);
    }

    private void OnDrawGizmos()
    {
        if(m_CollisionObject == null) return;

        switch(m_CollisionObject.CollisionShape.Type)
        {
            case ShapeType.Sphere:
                Gizmos.DrawWireSphere(transform.position, m_CollisionObject.CollisionShape.Radius);
                break;
        }
    }

    public void OnCollision(CollisionObject other)
    {
        
    }
}
