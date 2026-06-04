using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Health;
using Project.Gameplay.Stats;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(StatusEffectComponent))]
public class Entity : MonoBehaviour
{
    public HealthComponent Health {  get; private set; }
    public StatsComponent Stats { get; private set; }
    public StatusEffectComponent StatusEffects { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<HealthComponent>();
        Stats = GetComponent<StatsComponent>();
        StatusEffects = GetComponent<StatusEffectComponent>();
    }
}