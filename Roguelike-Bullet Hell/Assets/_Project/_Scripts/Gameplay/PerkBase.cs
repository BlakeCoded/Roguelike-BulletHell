using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Stats;
using Player;

public class PerkBase : MonoBehaviour
{
    [SerializeField] private StatType type;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponentInParent<PlayerStats>();

        if(stats != null)
        {
            Debug.Log(stats.GetStatValue(type));
        }
    }
}
