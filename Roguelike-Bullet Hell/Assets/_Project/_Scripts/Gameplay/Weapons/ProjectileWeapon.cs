using UnityEngine;
using Project.Gameplay.Stats;
using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;
using Project.Player;

public class ProjectileWeapon : WeaponInstance
{
    [SerializeField] private ProjectileBase projectilePrefab;

    protected override void OnUse(AttackContext attackContext)
    {
        ProjectileBase projectile = ObjectPoolManager.Get(projectilePrefab, firePoint.position, firePoint.rotation);

        float projectileSpeed = Owner.Stats.GetStatValue(StatType.ProjectileSpeed);
        float projectileSize = Owner.Stats.GetStatValue(StatType.Size);

        projectile.Initialize(attackContext, projectileSpeed, projectileSize);
    }
}