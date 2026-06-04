using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [RequireComponent(typeof(StatsComponent))]
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField] private Transform weaponPosition;
        [SerializeField] private Transform weaponFirePoint;
        [SerializeField] private WeaponData StartingWeapon;

        private WeaponInstance currentWeapon;
        private Entity Owner;


        private void Awake()
        {
            Owner = GetComponent<Entity>();
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
            currentWeapon?.Use();
        }

        public void AddOnHitEffect(IOnHitEffect onHitEffect)
        {
            if(currentWeapon != null)
            {
                currentWeapon.AddOnHitEffect(onHitEffect);
            }
        }
    }
}