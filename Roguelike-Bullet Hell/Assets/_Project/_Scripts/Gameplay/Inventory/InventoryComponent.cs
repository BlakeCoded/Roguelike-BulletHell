using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Project.Gameplay.Combat;
using Project.Gameplay.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryComponent : MonoBehaviour
    {
        public IReadOnlyList<InventoryItem> Items => inventoryItems;
        private List<InventoryItem> inventoryItems = new();

        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;

        public void Add(InventoryItemData itemData, CombatEntity owner)
        {
            InventoryItem item = new(itemData);

            foreach(StatModifierData stat in itemData.ItemStats)
            {
                StatModifier modifier = new StatModifier(stat.ModifierType, stat.Value, item);

                owner.Stats.AddStatModifier(stat.StatType, modifier);
            }

            foreach(IOnHitEffectData data in itemData.Effects)
            {
                IOnHitEffect effect = data.Create(item);

                owner.CombatEffects.AddOnHitEffect(effect);
            }

            inventoryItems.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void Remove(InventoryItem item,  CombatEntity owner)
        {
            owner.Stats.RemoveAllStatModifiers(item);
            owner.CombatEffects.RemoveAllOnHitEffects(item);

            inventoryItems.Remove(item);
            OnItemRemoved?.Invoke(item);
        }

        public void ClearAllItems(CombatEntity owner)
        {
            int ICount = inventoryItems.Count;

            for (int i = ICount - 1; i >= 0; i--)
            {
                Remove(inventoryItems[i], owner);
            }
        }
    }
}