using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Project.Gameplay.Combat
{
    public class GunWeapon : WeaponInstance
    {
        protected override void OnUse(AttackContext attackContext)
        {
            if(GameManager.Raycast(firePoint.position, firePoint.forward, 100f, Collision.CollisionLayer.Player, out RaycastHitData hit))
            {
                CombatEntity target = hit.CollisionObject.Entity;

                DamageContext damageContext = DamageResolver.CreateDamageContext(attackContext, target, hit.HitPoint, (target.CachedTransform.position - hit.HitPoint).normalized);

                DamageResolver.ProcessHit(damageContext);
            }
        }
    }
}