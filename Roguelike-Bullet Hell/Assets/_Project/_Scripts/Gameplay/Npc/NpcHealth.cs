using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class NpcHealth : MonoBehaviour, IDamageable
{
    public void TakeDamage(float amount)
    {
        Debug.Log(name + $" Took {amount} of Damage");
    }
}
