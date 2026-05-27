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

    public bool IsReleased { get; private set; }

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

        timer += GameTime.DeltaTime;
        
        if(timer >= AttackContext.LifeTime)
        {
            if(!IsReleased)
            {
                PoolManager.Instance.Release(gameObject);
                IsReleased = true;
            }
        }
    }

    protected abstract void HandleMovement();

    // Add layers etc to only interact with, WORLD, ENVIRONMENT, ENEMY
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            DamageContext damageContext = StatMath.CalculateDamage(AttackContext.Damage, AttackContext.CritChance, AttackContext.CritDamage);

            target.TakeDamage(damageContext.Damage);

            GameTextManager.Instance.ShowDamage(cachedTransform.position, damageContext);
        }

        if (!IsReleased)
        {
            PoolManager.Instance.Release(gameObject);
            IsReleased = true;
        }
    }

    public virtual void OnSpawn()
    {
        timer = 0;
        IsReleased = false;
    }

    public virtual void OnDespawn()
    {
        
    }
}
