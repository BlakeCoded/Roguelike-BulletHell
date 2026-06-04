using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/StatData")]
public class StatsData : ScriptableObject
{
    public List<StatDefinition> stats = new();
    public List<StatModifierData> modifiers = new();
}
