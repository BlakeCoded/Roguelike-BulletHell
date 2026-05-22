using System.Collections;
using System.Collections.Generic;
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
        public static float CalculateDamage(float damage, float critChance, float critDamageMultiplier)
        {
            if(critChance <= 0f) return damage;

            float roll = Random.Range(0f, 100f);

            if(roll < critChance) return damage * critDamageMultiplier;

            return damage;
        }
    }
}