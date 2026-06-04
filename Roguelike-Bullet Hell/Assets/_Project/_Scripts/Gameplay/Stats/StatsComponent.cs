using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Interfaces;
using System.ComponentModel;

namespace Project.Gameplay.Stats
{
    [RequireComponent(typeof(StatsInitializer))]
    public class StatsComponent : MonoBehaviour
    {
        private Dictionary<StatType, Stat> stats = new();

        public void AddStatModifier(StatType type, StatModifier modifier)
        {
            GetOrCreateStat(type).AddModifier(modifier);
        }

        public void RemoveStatModifier(StatType type, StatModifier modifier)
        {
            if (stats.TryGetValue(type, out Stat stat))
            {
                stat.RemoveModifier(modifier);
            }
        }

        public void RemoveAllStatModifiers(object source)
        {
            if (source == null) return;

            foreach(Stat stat in stats.Values)
            {
                stat.RemoveAllFromSource(source);
            }
        }

        public float GetStatValue(StatType type)
        {
            if (stats.TryGetValue(type, out Stat stat))
            {
                return stat.Value;
            }

            Debug.LogError($"StatType {type} was not found in Dictionary");

            return 0f;
        }

        public void SetBaseStat(StatType type, float value = 0f)
        {
            GetOrCreateStat(type).SetBaseValue(value);
        }

        private Stat GetOrCreateStat(StatType type, float defaultValue = 0f)
        {
            if(!stats.TryGetValue(type, out Stat stat))
            {
                stat = new Stat(defaultValue);
                stats[type] = stat;
            }

            return stat;
        }

        public IReadOnlyStat GetStat(StatType type)
        {
            return GetOrCreateStat(type);
        }
    }
}