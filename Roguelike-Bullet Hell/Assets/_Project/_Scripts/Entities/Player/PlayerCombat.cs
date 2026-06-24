using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    //[RequireComponent(typeof(StatsComponent))]
    //public class PlayerCombat : MonoBehaviour
    //{
    //    [SerializeField] private Transform weaponPosition;
    //    [SerializeField] private Transform prjectileSpawnPosition;
    //    [SerializeField] private WeaponData defaultWeapon;

    //    private WeaponInstance currentWeapon;
    //    private Entity Owner;
    //    private StatsComponent stats;

    //    private void Awake()
    //    {
    //        stats = GetComponent<StatsComponent>();
    //        Owner = GetComponent<Entity>();
    //        //EquipWeapon(defaultWeapon);
    //    }

    //    public void EquipWeapon(WeaponData weaponData)
    //    {
    //        if (currentWeapon.gameObject != null)
    //        {
    //            Destroy(currentWeapon.gameObject);
    //        }

    //        currentWeapon = Instantiate(weaponData.InstancePrefab, weaponPosition);

    //        currentWeapon.Initialize(Owner, weaponData, stats, prjectileSpawnPosition);
    //    }

    //    public void Attack()
    //    {
    //        currentWeapon?.Use();
    //    }
    //}
}