using System.Collections;
using System.Collections.Generic;
using Interfaces;
using OnHitEffect;
using Project.Gameplay.Combat;
using UnityEngine;

public class PhysicalDamageOnHitEffect : IOnHitEffect
{
    public PhysicalDamageOnHitEffect(object source, StackRule stackRule) : base(source, stackRule) { }

    private const float PERCENTAGE_DAMAGE = 0.05f; // 5%

    public override void Apply(DamageResult result, int stacks)
    {
        CombatEntity target = result.CombatContext.Target;

        float bonusDamage = result.DamageDealt * PERCENTAGE_DAMAGE;
        float totalDamage = bonusDamage * stacks;

        if(totalDamage < 1f) totalDamage = 1f;

        DamageContext damageContext = DamageContext.CreateSimpleDamageContext(result.CombatContext, totalDamage, DamageType.Physical);

        DamageResolver.ProcessDamage(damageContext);
    }
}