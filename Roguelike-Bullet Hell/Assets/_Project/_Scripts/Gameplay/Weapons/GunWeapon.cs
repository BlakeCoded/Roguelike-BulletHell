using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class GunWeapon : WeaponBehaviour
    {
        protected override void OnUse(AttackContext attackContext)
        {
            Ray ray = new Ray(firePoint.position, firePoint.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out IDamageable target))
                {
                    DamageContext dc = StatMath.CalculateDamage(attackContext.Damage, attackContext.CritChance, attackContext.CritDamage);

                    target.TakeDamage(dc.Damage);

                    GameTextManager.Instance.ShowDamage(hit.point, dc);

                    // crit sound, etc
                }
            }
        }
    }
}