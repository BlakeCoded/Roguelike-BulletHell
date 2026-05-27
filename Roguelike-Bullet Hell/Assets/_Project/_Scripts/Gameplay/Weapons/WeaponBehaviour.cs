using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using static Project.Gameplay.Stats.StatMath;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        protected WeaponData data;
        protected float lastUseTime;
        protected Transform firePoint;
        protected StatsComponent playerStats;

        private bool statsInitialized = false;

        public virtual void Initialize(WeaponData data, StatsComponent stats, Transform firePoint)
        {
            if (statsInitialized) return;

            statsInitialized = true;

            this.data = data;
            this.playerStats = stats;
            this.firePoint = firePoint;

            foreach (StatModifierData ModData in this.data.Modifiers)
            {
                AddModifier(ModData);
            }
        }

        public virtual bool CanUse()
        {
            return Time.time >= lastUseTime + 1f / ClampAttacksPerSecond(playerStats.GetStatValue(StatType.AttackSpeed));
        }

        public void Use()
        {
            if (!CanUse()) return;

            AttackContext context = CreateAttackContext();

            lastUseTime = Time.time;

            OnUse(context);
        }

        protected AttackContext CreateAttackContext()
        {
            AttackContext context = new AttackContext();

            context.Damage = playerStats.GetStatValue(StatType.Damage);

            context.AttacksPerSecond = ClampAttacksPerSecond(playerStats.GetStatValue(StatType.AttackSpeed));

            context.CritChance = ClampCritChance(playerStats.GetStatValue(StatType.CritChance));

            context.CritDamage = ClampCritDamage(playerStats.GetStatValue(StatType.CritDamage));

            context.ProjectileCount = ClampProjectileCount(playerStats.GetStatValue(StatType.ProjectileCount));

            context.ProjectileSpeed = ClampProjectileSpeed(playerStats.GetStatValue(StatType.ProjectileSpeed));

            context.Size = ClampSize(playerStats.GetStatValue(StatType.Size));

            context.Knockback = ClampKnockBack(playerStats.GetStatValue(StatType.Knockback));

            return context;
        }

        protected abstract void OnUse(AttackContext context);

        private void AddModifier(StatModifierData modifier)
        {
            playerStats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value, this));
        }
    }
}