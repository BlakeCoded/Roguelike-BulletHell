using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string WeaponName;
    public float BaseDamage;
    public float BaseAttackSpeed;
    public int BaseProjectileCount;
    public WeaponBehaviour BehaviourPrefab;
}