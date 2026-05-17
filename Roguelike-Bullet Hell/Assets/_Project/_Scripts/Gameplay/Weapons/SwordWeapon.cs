using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : WeaponBehaviour
{
    protected override void OnUse()
    {
        Debug.Log("Swing Sword");

        // melee overlap check
    }
}
