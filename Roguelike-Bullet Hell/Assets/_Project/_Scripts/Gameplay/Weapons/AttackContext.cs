using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Project.Gameplay.Stats;
using static Project.Gameplay.Stats.StatMath;

namespace Project.Gameplay.Combat
{
    public readonly struct AttackContext
    {
        // Ownership
        public CombatEntity Owner { get; }

        // Core Damage
        public float Damage { get; }
        public List<OnHitEffectEntry> OnHitEffects { get; }

        // Crit
        public float CritChance { get; }
        public float CritMultiplier { get; }
        
        // Projectile Creation
        public int ProjectileCount { get; }

        // General Attack Scaling
        public float Size { get; }

        // Utility
        public float Knockback { get; }
        

        public AttackContext(
            CombatEntity Owner,
            float Damage,
            List<OnHitEffectEntry> OnHitEffects,
            float CritChance,
            float CritMultiplier,
            int ProjectileCount,
            float Size,
            float Knockback)
        {
            this.Owner = Owner;
            this.Damage = Damage;
            this.OnHitEffects = OnHitEffects;
            this.CritChance = CritChance;
            this.CritMultiplier = CritMultiplier;
            this.ProjectileCount = ProjectileCount;
            this.Size = Size;
            this.Knockback = Knockback;
        }
    }

    public static class AttackContextFactory
    {
        public static AttackContext Create(CombatEntity owner)
        {
            return new AttackContext(
                Owner: owner,
                Damage: ClampDamage(owner.Stats.GetStatValue(StatType.Damage)),
                OnHitEffects: owner.CombatEffects.CreateOnHitEffectSnapshot(),
                CritChance: ClampCritChance(owner.Stats.GetStatValue(StatType.CritChance)),
                CritMultiplier: ClampCritMultiplier(owner.Stats.GetStatValue(StatType.CritDamage)),
                ProjectileCount: ClampProjectileCount(owner.Stats.GetStatValue(StatType.ProjectileCount)),
                Size: ClampSize(owner.Stats.GetStatValue(StatType.Size)),
                Knockback: ClampKnockBack(owner.Stats.GetStatValue(StatType.Knockback))
                );
        }
    }
}