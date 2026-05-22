using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(StatsComponent))]
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Transform weaponPosition;
        [SerializeField] private Transform prjectileSpawnPosition;
        [SerializeField] private WeaponData defaultWeapon;

        private WeaponBehaviour currentWeapon;
        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();

            //EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if (currentWeapon.gameObject != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            currentWeapon = Instantiate(weaponData.BehaviourPrefab, weaponPosition);

            currentWeapon.Initialize(weaponData, stats, prjectileSpawnPosition);
        }

        public void Attack()
        {
            currentWeapon?.Use();
        }
    }
}