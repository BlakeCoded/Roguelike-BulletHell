using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using Project.Gameplay.Stats;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IProjectile, IPoolable
{
    [SerializeField] protected float projectileSpeed;
    [SerializeField] protected float lifeTime;

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
        
        if(timer >= lifeTime)
        {
            if(!IsReleased)
            {
                ObjectPoolManager.Release(gameObject);
                IsReleased = true;
            }
        }
    }

    protected abstract void HandleMovement();

    // Add layers etc to only interact with, WORLD, ENVIRONMENT, ENEMY
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Entity>(out Entity target))
        {
            DamageContext damageContext = DamageResolver.CreateDamageContext(AttackContext, target, cachedTransform.position,
                                                                            (other.transform.position - cachedTransform.position).normalized);

            DamageResolver.ProcessHit(damageContext);

            if (!IsReleased)
            {
                ObjectPoolManager.Release(gameObject);
                IsReleased = true;
            }
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
