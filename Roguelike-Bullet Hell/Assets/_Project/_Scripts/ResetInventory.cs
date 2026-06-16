using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

public class ResetInventory : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ClearInventory(other);
    }

    private void ClearInventory(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox player))
        {
            player.Owner.Inventory.ClearAllItems(player.Owner);
        }
    }
}
