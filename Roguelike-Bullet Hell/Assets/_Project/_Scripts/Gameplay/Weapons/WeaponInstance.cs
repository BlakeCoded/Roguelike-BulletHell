using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using static Project.Gameplay.Stats.StatMath;
using UnityEngine;
using Interfaces;
using System.Linq;

namespace Project.Gameplay.Combat
{
    public abstract class WeaponInstance : MonoBehaviour
    {
        protected CombatEntity Owner;
        protected WeaponData data;
        protected Transform firePoint;

        private bool statsInitialized = false;

        protected float lastUseTime;

        public virtual void Initialize(CombatEntity owner, WeaponData data, Transform firePoint)
        {
            if (statsInitialized) return;

            this.Owner = owner;
            this.data = data;
            this.firePoint = firePoint;

            foreach (StatModifierData ModData in data.Modifiers)
            {
                AddModifier(ModData);
            }

            statsInitialized = true;
        }

        public virtual bool CanUse()
        {
            return Time.time >= lastUseTime + 1f / ClampAttacksPerSecond(Owner.Stats.GetStatValue(StatType.AttackSpeed));
        }

        public void Use(AttackContext context)
        {
            if (!CanUse()) return;

            lastUseTime = Time.time;

            OnUse(context);
        }

        protected abstract void OnUse(AttackContext context);

        private void AddModifier(StatModifierData modifier)
        {
            Owner.Stats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value, this));
        }
    }
}