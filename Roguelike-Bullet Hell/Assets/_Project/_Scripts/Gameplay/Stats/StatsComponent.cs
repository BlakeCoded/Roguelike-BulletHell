using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Interfaces;
using System.ComponentModel;

namespace Project.Gameplay.Stats
{
    public class StatsComponent : MonoBehaviour
    {
        private Dictionary<StatType, Stat> stats = new();

        private void AddStat(StatType type, Stat stat)
        {
            if(stats.ContainsKey(type))
            {
                Debug.LogWarning($"Duplicate stat {type} on {name}");
                return; // do not writeover exisiting stat
            }

            stats[type] = stat;
        }

        public bool HasStat(StatType type)
        {
            return stats.ContainsKey(type);
        }

        public bool TryGetStatValue(StatType type, out float value)
        {
            value = 0f;

            if (stats.TryGetValue(type, out Stat stat))
            {
                value = stat.Value;
                return true;
            }

            return false;
        }

        public void AddStatModifier(StatType type, StatModifier modifier)
        {
            if (stats.TryGetValue(type, out Stat stat))
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
            if (stats.TryGetValue(type, out Stat stat) && source is not null)
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
            if(!stats.TryGetValue(type, out Stat stat))
            {
                stat = new Stat(value);
                stats[type] = stat;
                return;
            }

            stats[type].SetBaseValue(value);
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

        public Stat GetStat(StatType type)
        {
            return stats[type];
        }
    }
}