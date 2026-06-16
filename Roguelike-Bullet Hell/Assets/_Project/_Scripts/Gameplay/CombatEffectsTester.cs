using System.Collections;
using UnityEngine;
using Project.Gameplay.Combat;
using System.Collections.Generic;

public class CombatEffectsTester : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> inventoryItemData;

    private void OnTriggerEnter(Collider other)
    { 
        AddItemToInventory(other);
    }

    private void AddItemToInventory(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox player))
        {
            foreach(InventoryItemData data in inventoryItemData)
            {
                player.Owner.Inventory.Add(data, player.Owner);
            }
        }
    }
}
