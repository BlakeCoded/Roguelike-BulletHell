using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    public static class StatMath
    {
        public static float ClampAttacksPerSecond(float value) => Mathf.Max(0.01f, value);
        public static float ClampCritChance(float value) => Mathf.Max(0f, value);
        public static float ClampCritDamage(float value) => Mathf.Max(1f, value);
        public static float ClampProjectileCount(float value) => Mathf.Max(1f, value);
        public static float ClampProjectileSpeed(float value) => Mathf.Max(0.01f, value);
        public static float ClampSize(float value) => Mathf.Max(1f, value);
        public static float ClampKnockBack(float value) => Mathf.Max(0f, value);
        public static DamageContext CalculateDamage(float damage, float critChance, float critDamageMultiplier)
        {
            DamageContext dc = new();

            float roll = Random.Range(0f, 100f);

            dc.IsCrit = roll < critChance;

            dc.Damage = dc.IsCrit ? damage * critDamageMultiplier : damage;

            return dc;
        }
    }
}