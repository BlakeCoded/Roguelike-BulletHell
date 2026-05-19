using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;

public class PerkBase : MonoBehaviour
{
    [SerializeField] private StatType type;

    private void OnTriggerEnter(Collider other)
    {
        StatsComponent stats = other.GetComponentInParent<StatsComponent>();

        if(stats != null)
        {
            Debug.Log($"{type}" + stats.GetStatValue(type));
        }
    }
}
