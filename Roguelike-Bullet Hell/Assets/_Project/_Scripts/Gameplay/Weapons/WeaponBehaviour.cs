using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using static Project.Gameplay.Stats.StatMath;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(StatsComponent))]
    public abstract class WeaponBehaviour : MonoBehaviour
    {
        protected WeaponData data;
        protected float lastUseTime;
        protected Transform firePoint;
        protected StatsComponent playerStats;
        protected StatsComponent weaponStats;

        private bool statsInitialized = false;

        private void Awake()
        {
            weaponStats = GetComponent<StatsComponent>();
        }

        private void InitializeBaseStats()
        {
            if (statsInitialized) return;

            weaponStats.SetBaseStat(StatType.Damage, this.data.WeaponDamage);
            weaponStats.SetBaseStat(StatType.AttackSpeed, this.data.WeaponAttacksPerSecond);
            weaponStats.SetBaseStat(StatType.CritChance, this.data.WeaponCritChance);
            weaponStats.SetBaseStat(StatType.CritDamage, this.data.WeaponCritDamage);
            weaponStats.SetBaseStat(StatType.ProjectileCount, this.data.WeaponProjectileCount);
            weaponStats.SetBaseStat(StatType.ProjectileSpeed, this.data.WeaponProjectileSpeed);
            weaponStats.SetBaseStat(StatType.Size, this.data.WeaponAreaSize);
            weaponStats.SetBaseStat(StatType.Knockback, this.data.WeaponKnockBack);

            statsInitialized = true;
        }

        public virtual void Initialize(WeaponData data, StatsComponent stats, Transform firePoint)
        {
            this.data = data;
            this.playerStats = stats;
            this.firePoint = firePoint;

            InitializeBaseStats();
        }

        public virtual bool CanUse()
        {
            return Time.time >= lastUseTime + 1f / ClampAttacksPerSecond(weaponStats.GetStatValue(StatType.AttackSpeed)
                                                       + playerStats.GetStatValue(StatType.AttackSpeed)); ;
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

            context.Damage = weaponStats.GetStatValue(StatType.Damage)
                           + playerStats.GetStatValue(StatType.Damage);

            context.AttacksPerSecond = ClampAttacksPerSecond(weaponStats.GetStatValue(StatType.AttackSpeed)
                                                      + playerStats.GetStatValue(StatType.AttackSpeed));

            context.CritChance = ClampCritChance(weaponStats.GetStatValue(StatType.CritChance)
                                               + playerStats.GetStatValue(StatType.CritChance));

            context.CritDamage = ClampCritDamage(weaponStats.GetStatValue(StatType.CritDamage)
                                               + playerStats.GetStatValue(StatType.CritDamage));

            context.ProjectileCount = ClampProjectileCount(weaponStats.GetStatValue(StatType.ProjectileCount)
                                                         + playerStats.GetStatValue(StatType.ProjectileCount));

            context.ProjectileSpeed = ClampProjectileSpeed(weaponStats.GetStatValue(StatType.ProjectileSpeed)
                                                         + playerStats.GetStatValue(StatType.ProjectileSpeed));

            context.Size = ClampSize(weaponStats.GetStatValue(StatType.Size)
                                   + playerStats.GetStatValue(StatType.Size));

            context.Knockback = ClampKnockBack(weaponStats.GetStatValue(StatType.Knockback)
                                             + playerStats.GetStatValue(StatType.Knockback));

            return context;
        }

        protected abstract void OnUse(AttackContext context);
    }
}