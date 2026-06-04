using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    [System.Serializable]
    public enum StatType
    {
        // Offensive Stats
        Damage,
        AttackSpeed,
        CritChance,
        CritDamage,

        // Projectile Stats
        ProjectileCount,
        ProjectileSpeed,
        Knockback,

        // Movement Stats
        MoveSpeed,

        // Defensive Stats
        BonusHealth,
        Armor,
        HealthRegen,

        // Utility Stats
        CooldownReduction,
        Luck,

        // Scaling Stats
        Size,

        // Timing Stats
        LifeTime,
    }

    [System.Serializable]
    public enum StatModifierType
    {
        Flat,
        AdditivePercent,
        Multiplyer
    }

    [System.Serializable]
    public class StatDefinition
    {
        public StatType Type;
        public float BaseValue;
    }

    /// <summary>
    /// Serialized configuration data for a stat modifier.
    /// Used in ScriptableObjects, inspector data, save data, etc.
    /// Does NOT represent an active runtime modifier.
    /// </summary>
    [System.Serializable]
    public class StatModifierData
    {
        public StatType StatType;
        public float Value;
        public StatModifierType ModifierType;
    }

    /// <summary>
    /// Runtime stat modifier applied to a Stat.
    /// Contains gameplay state such as source ownership.
    /// Used during active gameplay calculations.
    /// </summary>
    public class StatModifier
    {
        public StatModifierType Type;
        public float Value;
        
        public object Source;

        public StatModifier(StatModifierType type, float value, object source = null)
        {
            Type = type;
            Value = value;
            Source = source;
        }
    }
}