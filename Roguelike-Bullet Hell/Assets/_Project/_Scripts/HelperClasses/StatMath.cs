using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    public static class StatMath
    {
        public static float ClampDamage(float value) => Mathf.Max(0.1f, value);
        public static float ClampAttacksPerSecond(float value) => Mathf.Max(0.01f, value);
        public static float ClampCritChance(float value) => Mathf.Max(0f, value);
        public static float ClampCritMultiplier(float value) => Mathf.Max(1f, value);
        public static int ClampProjectileCount(float value) => Mathf.RoundToInt(Mathf.Max(1f, value));
        public static float ClampProjectileSpeed(float value) => Mathf.Max(0.01f, value);
        public static float ClampSize(float value) => Mathf.Max(1f, value);
        public static float ClampKnockBack(float value) => Mathf.Max(0f, value);
        public static float ClampLifeTime(float value) => Mathf.Max(0.1f, value);
    }
}