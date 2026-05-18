using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using Project.Player;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    protected WeaponData data;
    protected StatsComponent stats;
    protected float lastUseTime;
    protected Transform firePoint;

    protected float Damage => data.BaseDamage + stats.GetStatValue(StatType.Damage);
    protected float FinalAttackSpeed => data.BaseAttackSpeed + stats.GetStatValue(StatType.AttackSpeed);

    public virtual void Initialize(WeaponData data, StatsComponent stats, Transform firePoint)
    {
        this.data = data;
        this.stats = stats;
        this.firePoint = firePoint;
    }

    public virtual bool CanUse()
    {
        return Time.time >= lastUseTime + FinalAttackSpeed;
    }

    public void Use()
    {
        if(!CanUse()) return;

        lastUseTime = Time.time;

        OnUse();
    }

    protected abstract void OnUse();
}