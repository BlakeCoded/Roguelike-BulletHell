using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

public class StatsInitializer : MonoBehaviour
{
    [SerializeField] List<StatDefinition> startingStats = new();

    private void Awake()
    {
        StatsComponent stats = GetComponent<StatsComponent>();

        foreach (var sd in startingStats)
        {
            stats.SetBaseStat(sd.Type, sd.BaseValue);
        }

        startingStats = null;
    }
}
