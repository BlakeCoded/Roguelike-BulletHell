using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public string WeaponName;
        [Range(1f, 100f)] public float WeaponDamage;
        [Range(0.01f, 10f)]public float WeaponAttacksPerSecond;
        [Range(0f, 100f)] public float WeaponCritChance;
        [Range(1f, 10f)] public float WeaponCritDamage;
        [Range(1f, 10)] public float WeaponProjectileCount;
        [Range(0.01f, 50f)] public float WeaponProjectileSpeed;
        [Range(1f, 5f)] public float WeaponAreaSize;
        [Range(0f, 10f)] public float WeaponKnockBack;
        public WeaponBehaviour BehaviourPrefab;
    }
}