using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        public string WeaponName;
        public List<StatModifierData> Modifiers;
        public WeaponInstance InstancePrefab;
    }
}