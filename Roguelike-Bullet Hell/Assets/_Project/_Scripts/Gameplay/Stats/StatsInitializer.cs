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

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _isInitialized = true;

        StatsComponent stats = GetComponent<StatsComponent>();

        foreach (var sd in statsData.stats)
        {
            stats.SetBaseStat(sd.Type, sd.BaseValue);
        }

        foreach (var sd in statsData.modifiers)
        {
            stats.AddStatModifier(sd.StatType, new StatModifier(sd.ModifierType, sd.Value));
        }
    }
}
