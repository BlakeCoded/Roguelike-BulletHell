using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

public class StatsInitializer : MonoBehaviour, IInitializable
{
    [SerializeField] StatsData statsData;

    public bool IsInitialized => _isInitialized;
    private bool _isInitialized = false;

    private StatsComponent stats;

    private void Awake()
    {
        Init(GetComponent<StatsComponent>());
    }

    public void Init(StatsComponent stats)
    {
        _isInitialized = true;

        foreach (var sd in statsData.stats)
        {
            stats.SetBaseStat(sd.Type, sd.BaseValue);
        }

        foreach (var sd in statsData.modifiers)
        {
            stats.AddStatModifier(sd.StatType, new StatModifier(sd.ModifierType, sd.Value));
        }
    }

    public void Init()
    {
        
    }
}
