using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using Project.Gameplay.Health;
using Project.Gameplay.Movement;
using Project.Gameplay.Stats;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(StatusEffectComponent))]
[RequireComponent(typeof(MovementComponentBase))]
public class Entity : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public HealthComponent Health {  get; private set; }
    public StatsComponent Stats { get; private set; }
    public StatusEffectComponent StatusEffects { get; private set; }
    public MovementComponentBase MovementComponent { get; private set; }

    protected virtual void Awake()
    {
        Transform = transform;
        Health = GetComponent<HealthComponent>();
        Stats = GetComponent<StatsComponent>();
        StatusEffects = GetComponent<StatusEffectComponent>();
        MovementComponent = GetComponent<MovementComponentBase>();
    }
}