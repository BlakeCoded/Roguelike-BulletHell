using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(StatsComponent))]
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Transform weaponPosition;
        [SerializeField] private Transform weaponFirePoint;
        [SerializeField] private WeaponData weaponData;

        private WeaponBehaviour currentWeapon;
        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();

            EquipWeapon(weaponData);
        }

        public void EquipWeapon(WeaponData data)
        {
            if(currentWeapon is not null)
            {
                Destroy(currentWeapon.gameObject);
            }

            currentWeapon = Instantiate(data.BehaviourPrefab, weaponPosition);

            currentWeapon.Initialize(data, stats, weaponFirePoint);
        }

        public void Attack()
        {
            currentWeapon?.Use();
        }
    }
}