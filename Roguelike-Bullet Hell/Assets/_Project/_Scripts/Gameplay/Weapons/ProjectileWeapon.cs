using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void OnUse()
    {
        Debug.Log("Spawning projectile");

        // projectile count and position etc

        Instantiate(projectilePrefab, transform.position, transform.rotation);
    }
}
