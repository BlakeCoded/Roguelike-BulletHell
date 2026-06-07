using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

public class DamageOnHitEffect : IOnHitEffect
{
    public string Id => "Damage";
    public StackRule StackRule => StackRule.Stackable;

    public void Apply(DamageResult result, int count)
    {
        Entity target = result.CombatContext.Target;

        float damage = (result.DamageDealt * 0.05f) * count;

        DamageContext dc = new DamageContext(result.CombatContext, damage, DamageResolver.EmptyOnHitEffects, false, target.Transform.position, Vector3.zero, 0f);

        DamageResolver.ProcessHit(dc);
    }
}
