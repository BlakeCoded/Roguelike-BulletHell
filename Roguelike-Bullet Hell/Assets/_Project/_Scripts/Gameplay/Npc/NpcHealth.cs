using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class NpcHealth : MonoBehaviour, IDamageable
{
    public void Heal(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log(name + $" Took {amount} of Damage");
    }
}
