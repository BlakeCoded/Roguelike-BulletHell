using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;
using Project.Gameplay.Buffs;

public class PerkBase : MonoBehaviour
{
    [SerializeField] private StatModifierData modifier;
    [SerializeField] private List<BuffData> buffs;

    private void OnTriggerEnter(Collider other)
    {
        //AddStat(other);

        AddBuffs(other);
    }

    private void AddStat(Collider other)
    {
        StatsComponent stats = other.GetComponentInParent<StatsComponent>();

        if (stats != null)
        {
            Debug.Log(stats.GetStatValue(StatType.AttackSpeed));
            stats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value));
            Debug.Log(stats.GetStatValue(StatType.AttackSpeed));
        }
    }

    private void AddBuffs(Collider other)
    {
        BuffComponent bc = other.GetComponentInParent<BuffComponent>();

        foreach (BuffData buff in buffs)
        {
            bc.AddBuff(buff);
        }
    }
}
