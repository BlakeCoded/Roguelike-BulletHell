using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IProjectile
{
    public float Damage { get; private set; }
    public float MoveSpeed {  get; private set; } 
    public float Lifetime { get; private set; }

    protected float timer;

    public void Initialize(float damage, float moveSpeed, float lifeTime)
    {
        Damage = damage;
        MoveSpeed = moveSpeed;
        Lifetime = lifeTime;
    }

    protected virtual void Update()
    {
        HandleMovement();

        timer += Time.deltaTime;
        
        if(timer >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void HandleMovement();

    // Add layers etc to only interact with, WORLD, ENVIRONMENT, ENEMY
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
