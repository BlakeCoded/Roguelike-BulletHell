using System.Collections;
using System.Collections.Generic;
using Interfaces;
using OnHitEffect;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(StatsComponent))]
    public class PlayerWeaponHolder : MonoBehaviour
    {
        private CombatEntity Owner;
        private WeaponInstance currentWeapon;

        [SerializeField] private Transform weaponPosition;
        [SerializeField] private Transform weaponFirePoint;
        [SerializeField] private WeaponData StartingWeapon;

        private void Awake()
        {
            Owner = GetComponent<CombatEntity>();
        }

        private void Start()
        {
            EquipWeapon(StartingWeapon);
        }

        public void EquipWeapon(WeaponData data)
        {
            if(currentWeapon != null)
            {
                Owner.Stats.RemoveAllStatModifiers(currentWeapon);

                Destroy(currentWeapon.gameObject);
            }

            currentWeapon = Instantiate(data.InstancePrefab, weaponPosition);
            currentWeapon.Initialize(Owner, data, weaponFirePoint);
        }

        public void Attack()
        {
            currentWeapon.Use(AttackContextFactory.Create(Owner));
        }
    }
}