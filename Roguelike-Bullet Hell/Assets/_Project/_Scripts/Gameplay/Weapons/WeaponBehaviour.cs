using System.Collections;
using System.Collections.Generic;
using Project.Player;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    protected WeaponData data;
    protected PlayerCombat owner;

    protected float lastUseTime;

    public virtual void Initialize(PlayerCombat owner, WeaponData data)
    {
        this.owner = owner;
        this.data = data;
    }

    public virtual bool CanUse()
    {
        return Time.time >= lastUseTime + data.cooldown;
    }

    public void Use()
    {
        if(!CanUse()) return;

        lastUseTime = Time.time;

        OnUse();
    }

    protected abstract void OnUse();
}