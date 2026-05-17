using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour, IInitializable
    {
        [SerializeField] private Transform weaponPosition;
        [SerializeField] private WeaponData defaultWeapon;

        private WeaponBehaviour currentWeapon;

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(WeaponData weaponData)
        {
            if(currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            currentWeapon = Instantiate(weaponData.behaviourPrefab, weaponPosition);

            currentWeapon.Initialize(this, weaponData);
        }

        public void Attack()
        {
            currentWeapon?.Use();
        }
    }
}