using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

[RequireComponent(typeof(StatsComponent))]
public abstract class WeaponBehaviour : MonoBehaviour
{
    protected WeaponData data;
    protected float lastUseTime;
    protected Transform firePoint;
    protected StatsComponent playerStats;
    protected StatsComponent weaponStats;

    protected float Damage => weaponStats.GetStatValue(StatType.Damage) + playerStats.GetStatValue(StatType.Damage);
    protected float AttackSpeed => Mathf.Max(0.01f ,data.BaseAttackSpeed + playerStats.GetStatValue(StatType.AttackSpeed)); // fix this to use weapon stats attack speed + base + playerstats
    protected float Cooldown => 1f / AttackSpeed;

    public virtual void Initialize(WeaponData data, StatsComponent stats, Transform firePoint)
    {
        this.data = data;
        this.playerStats = stats;
        this.firePoint = firePoint;

        weaponStats = GetComponent<StatsComponent>();

        weaponStats.SetBaseStat(StatType.Damage, data.BaseDamage);
    }

    public virtual bool CanUse()
    {
        return Time.time >= lastUseTime + Cooldown;
    }

    public void Use()
    {
        if(!CanUse()) return;

        lastUseTime = Time.time;

        OnUse();
    }

    protected abstract void OnUse();
}