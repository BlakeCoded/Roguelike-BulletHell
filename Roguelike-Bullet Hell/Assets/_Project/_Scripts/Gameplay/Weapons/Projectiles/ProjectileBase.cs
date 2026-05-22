using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using Project.Gameplay.Stats;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IProjectile, IPoolable
{
    public AttackContext AttackContext { get; private set; }

    protected float timer;

    protected Transform cachedTransform;

    private void Awake()
    {
        cachedTransform = transform;
    }

    public void Initialize(AttackContext context)
    {
        AttackContext = context;

        OnInitalize();
    }

    protected abstract void OnInitalize();

    protected virtual void Update()
    {
        HandleMovement();

        timer += Time.deltaTime;
        
        if(timer >= AttackContext.LifeTime)
        {
            PoolManager.Instance.Release(gameObject);
        }
    }

    protected abstract void HandleMovement();

    // Add layers etc to only interact with, WORLD, ENVIRONMENT, ENEMY
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(StatMath.CalculateDamage(AttackContext.Damage, AttackContext.CritChance, AttackContext.CritDamage));
        }

        PoolManager.Instance.Release(gameObject);
    }

    public virtual void OnSpawn()
    {
        timer = 0;
    }

    public virtual void OnDespawn()
    {
        
    }
}
