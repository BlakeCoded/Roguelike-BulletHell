using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using static Project.Gameplay.Stats.StatMath;
using UnityEngine;
using Interfaces;

namespace Project.Gameplay.Combat
{
    public abstract class WeaponInstance : MonoBehaviour
    {
        protected Entity Owner;
        protected WeaponData data;
        protected float lastUseTime;
        protected Transform firePoint;

        private bool statsInitialized = false;
        private readonly List<IOnHitEffect> onHitEffects = new();

        public virtual void Initialize(Entity owner, WeaponData data, Transform firePoint)
        {
            if (statsInitialized) return;

            this.Owner = owner;
            this.data = data;
            this.firePoint = firePoint;

            foreach (StatModifierData ModData in this.data.Modifiers)
            {
                AddModifier(ModData);
            }

            statsInitialized = true;
        }

        public virtual bool CanUse()
        {
            return Time.time >= lastUseTime + 1f / ClampAttacksPerSecond(Owner.Stats.GetStatValue(StatType.AttackSpeed));
        }

        public void Use()
        {
            if (!CanUse()) return;

            AttackContext context = CreateAttackContext();

            lastUseTime = Time.time;

            OnUse(context);
        }

        public void AddOnHitEffect(IOnHitEffect effect)
        {
            onHitEffects.Add(effect);
        }

        protected AttackContext CreateAttackContext()
        {
            return new AttackContext(
                Owner: Owner,
                Damage: ClampDamage(Owner.Stats.GetStatValue(StatType.Damage)),
                OnHitEffects: onHitEffects,
                CritChance: ClampCritChance(Owner.Stats.GetStatValue(StatType.CritChance)),
                CritDamage: ClampCritDamage(Owner.Stats.GetStatValue(StatType.CritDamage)),
                ProjectileCount: ClampProjectileCount(Owner.Stats.GetStatValue(StatType.ProjectileCount)),
                Size: ClampSize(Owner.Stats.GetStatValue(StatType.Size)),
                Knockback: ClampKnockBack(Owner.Stats.GetStatValue(StatType.Knockback))
                );
        }

        protected abstract void OnUse(AttackContext context);

        private void AddModifier(StatModifierData modifier)
        {
            Owner.Stats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value, this));
        }
    }
}