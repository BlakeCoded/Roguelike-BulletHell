using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

public class StatsInitializer : MonoBehaviour
{
    [SerializeField] StatsData statsData;

    private void Awake()
    {
        Initialize(GetComponent<StatsComponent>());
    }

    public void Initialize(StatsComponent stats)
    {
        foreach (StatDefinition sd in statsData.stats)
        {
            stats.SetBaseStat(sd.Type, sd.BaseValue);
        }

        foreach (StatModifierData modifier in statsData.modifiers)
        {
            stats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value));
        }
    }
}
