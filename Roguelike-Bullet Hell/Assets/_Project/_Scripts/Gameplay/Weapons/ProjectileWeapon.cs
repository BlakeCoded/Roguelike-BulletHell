using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;
using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;

public class ProjectileWeapon : WeaponBehaviour
{
    [SerializeField] private ProjectileBase projectilePrefab;

    [SerializeField] private float baseProjectileSpeed;

    [SerializeField] private float baseLifeTime;

    protected override void OnUse(AttackContext attackContext)
    {
        attackContext.LifeTime = baseLifeTime;

        ProjectileBase projectile = PoolManager.Instance.Get(projectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Initialize(attackContext);
    }
}