using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Stats;
using UnityEditor.IMGUI.Controls;
using Unity.Collections.LowLevel.Unsafe;

namespace Player
{
    public class PlayerStats : MonoBehaviour, IInitializable
    {
        private Dictionary<StatType, Stat> stats;

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            stats = new Dictionary<StatType, Stat>();

            AddStat(StatType.Damage, new Stat(10f, 1f));
            AddStat(StatType.AttackSpeed, new Stat(1f, 0.1f));
            AddStat(StatType.MoveSpeed, new Stat(5f, 1f));
            AddStat(StatType.CritChance, new Stat(0.5f, 0f));
            AddStat(StatType.CritDamage, new Stat(1.5f, 1f));
            AddStat(StatType.Health, new Stat(10f, 0f));
            AddStat(StatType.Armor, new Stat(0f, 0f));
            AddStat(StatType.Luck, new Stat(1f, 0f));
            AddStat(StatType.ProjectileCount, new Stat(1f, 1f));
            AddStat(StatType.ProjectileSpeed, new Stat(10f, 1f));
            AddStat(StatType.CooldownReduction, new Stat(0f, 0f));
            AddStat(StatType.AreaSize, new Stat(1f, 1f));
            AddStat(StatType.KnockBack, new Stat(0f, 0f));

            IsInitialized = true;
        }

        private void AddStat(StatType type, Stat stat)
        {
            stats.Add(type, stat);
        }

        public void AddStatModifier(StatType type, StatModifier modifier)
        {
            if(stats.TryGetValue(type, out Stat stat))
            {
                stat.AddModifier(modifier);
            }
        }

        public void RemoveStatModifier(StatType type, StatModifier modifier)
        {
            if (stats.TryGetValue(type, out Stat stat))
            {
                stat.RemoveModifier(modifier);
            }
        }

        public void RemoveAllStatModifiers(StatType type, object source)
        {
            if(stats.TryGetValue(type, out Stat stat) && source is not null)
            {
                stat.RemoveAllFromSource(source);
            }
        }

        public float GetStatValue(StatType type)
        {
            if(stats.TryGetValue(type, out Stat stat))
            {
                return stat.Value;
            }

            Debug.LogError($"StatType {type} was not found in Dictionary");

            return 0f;
        }
    }
}