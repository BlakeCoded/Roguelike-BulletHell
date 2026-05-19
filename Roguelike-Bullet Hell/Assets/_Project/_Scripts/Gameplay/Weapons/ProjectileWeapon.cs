using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

public class ProjectileWeapon : WeaponBehaviour
{
    [SerializeField] private ProjectileBase projectilePrefab;

    [SerializeField] private float baseProjectileSpeed;

    [SerializeField] private float baseLifeTime;

    protected float ProjectileSpeed => baseProjectileSpeed + playerStats.GetStatValue(StatType.ProjectileSpeed);

    protected override void OnUse()
    {
        Debug.Log("Spawning projectile");

        // projectile count and position etc

        ProjectileBase projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        projectile.Initialize(Damage, ProjectileSpeed, baseLifeTime);
    }
}
