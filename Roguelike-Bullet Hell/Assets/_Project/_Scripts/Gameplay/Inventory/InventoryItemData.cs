using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;
using Project.Gameplay.Combat;

[CreateAssetMenu(menuName = "Inventory Item/Item")]
public class InventoryItemData : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public List<StatModifierData> ItemStats;
    public List<IOnHitEffectData> Effects;
}

public class InventoryItem
{
    public InventoryItemData Data { get; }

    public InventoryItem(InventoryItemData data)
    {
        Data = data;
    }
}