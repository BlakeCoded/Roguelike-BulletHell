using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

public class DamageOnHitEffect : IOnHitEffect
{
    public void Apply(DamageContext context, DamageResult result)
    {
        float damage = result.DamageDealt * 0.1f;

        DamageContext damageContext = DamageContext.Create(context, damage);

        DamageResolver.ProcessHit(damageContext);
    }
}
