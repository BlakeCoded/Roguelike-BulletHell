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
                if (hit.collider.TryGetComponent(out CombatEntity target))
                {
                    DamageContext damageContext = DamageResolver.CreateDamageContext(attackContext, target, hit.point, (hit.transform.position - hit.point).normalized);
                        
                    DamageResolver.ProcessHit(damageContext);
                }
            }
        }
    }
}