using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public float damage;
    public float cooldown;

    public WeaponBehaviour behaviourPrefab;
}