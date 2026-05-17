using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    [System.Serializable]
    public enum StatModifierType
    {
        Flat,
        AddativePercent,
        Multiplyer
    }

    [System.Serializable]
    public enum StatType
    {
        Damage,
        AttackSpeed,
        MoveSpeed,
        CritChance,
        CritDamage,
        Health,
        Armor,
        Luck,
        ProjectileCount,
        ProjectileSpeed,
        CooldownReduction,
        AreaSize, 
        KnockBack
    }
}