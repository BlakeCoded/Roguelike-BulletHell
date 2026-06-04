using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class SwordWeapon : WeaponInstance
    {
        protected override void OnUse(AttackContext attackContext)
        {
            Debug.Log("Swing Sword");

            // melee overlap check
        }
    }
}