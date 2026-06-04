using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class GunWeapon : WeaponInstance
    {
        protected override void OnUse(AttackContext attackContext)
        {
            Ray ray = new Ray(firePoint.position, firePoint.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Entity target))
                {
                    Vector3 hitPoint = hit.point;

                    DamageContext damageContext = DamageResolver.CreateDamageContext(attackContext, target, hitPoint, (hit.transform.position - hitPoint).normalized);
                        
                    DamageResolver.ProcessHit(damageContext);

                    // crit sound, etc
                }
            }
        }
    }
}