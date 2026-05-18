using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class GunWeapon : WeaponBehaviour
{
    protected override void OnUse()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(Damage);
            }
        }
    }
}
