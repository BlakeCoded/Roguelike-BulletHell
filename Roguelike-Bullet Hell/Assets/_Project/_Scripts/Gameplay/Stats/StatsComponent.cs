using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    public class StatsComponent : MonoBehaviour, IInitializable
    {
        private Dictionary<StatType, Stat> stats;

        [SerializeField] private List<StatDefinition> startingStats = new();

        public bool IsInitialized { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            stats = new();

            foreach (StatDefinition sd in startingStats)
            {
                AddStat(sd.Type, new Stat(sd.BaseValue, sd.MinValue));
            }

            IsInitialized = true;
        }

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
    }
}