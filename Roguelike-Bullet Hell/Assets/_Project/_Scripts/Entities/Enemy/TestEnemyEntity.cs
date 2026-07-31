using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using UnityEngine;

public class TestEnemyEntity : CombatEntity
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        SyncCollisionTransform();
    }

    public override void OnHit(CollisionObject other)
    {
        
    }
}
