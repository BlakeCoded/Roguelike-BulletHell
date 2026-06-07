using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;
using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;

public class ProjectileWeapon : WeaponInstance
{
    [SerializeField] private ProjectileBase projectilePrefab;

    protected override void OnUse(AttackContext attackContext)
    {
        ProjectileBase projectile = ObjectPoolManager.Get(projectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Initialize(attackContext);
    }
}