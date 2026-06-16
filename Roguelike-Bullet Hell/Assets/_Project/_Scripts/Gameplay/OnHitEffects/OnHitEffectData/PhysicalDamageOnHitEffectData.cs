using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using UnityEngine;

namespace OnHitEffect
{
    [CreateAssetMenu(menuName = "OnHitEffectData/PhysicalDamageOnHitEffect")]
    public class PhysicalDamageOnHitEffectData : IOnHitEffectData
    {
        public override StackRule StackRule => StackRule.Stackable;

        public override IOnHitEffect Create(object source)
        {
            return new PhysicalDamageOnHitEffect(source, StackRule);
        }
    }
}