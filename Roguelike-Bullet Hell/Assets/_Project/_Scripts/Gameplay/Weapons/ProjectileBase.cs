using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, IProjectile
{
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float MoveSpeed {  get; private set; } 
    [field: SerializeField] public float Lifetime { get; private set; }

    protected float timer;

    public void Initialize(float damage, float moveSpeed, float lifeTime)
    {
        Damage = damage;
        MoveSpeed = moveSpeed;
        Lifetime = lifeTime;
    }

    private void Update()
    {
        transform.MoveByXZ(Vector2.up * MoveSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        
        if(timer >= Lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}
