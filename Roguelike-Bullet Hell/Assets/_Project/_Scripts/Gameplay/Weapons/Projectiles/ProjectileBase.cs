using Collision;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public abstract class ProjectileBase : MonoBehaviour, IProjectile, IPoolable, ICollisionHandler
{
    protected Transform cachedTransform;

    // IProjectile
    public int Index { get; set; }
    public AttackContext AttackContext { get; private set; }
    public CollisionObject CollisionObject { get; private set; }

    //
    protected float projectileSpeed { get; private set; }
    protected float projectileSize { get; private set; }
    public bool IsReleased { get; private set; }

    [SerializeField] protected float lifeTime;
    protected float timer;

    private Vector3 baseScale;

    private void Awake()
    {
        cachedTransform = transform;
        baseScale = cachedTransform.localScale;

        CollisionObject = new CollisionObject
        {
            CollisionHandler = this,
            CollisionShape = new CollisionShape(),
            Layer = CollisionLayer.PlayerProjectiles
        };

        CollisionObject.CollisionShape.Type = ShapeType.Sphere;
    }

    public void Initialize(AttackContext context, float projectileSpeed, float projectileSize)
    {
        AttackContext = context;

        this.projectileSpeed = projectileSpeed;
        this.projectileSize = projectileSize;

        cachedTransform.localScale = projectileSize * baseScale;

        CollisionObject.Entity = context.Owner;
        CollisionObject.CollisionShape.Radius = projectileSize * cachedTransform.localScale.x;

        OnInitalize();
    }

    protected abstract void OnInitalize();

    public virtual void Tick(float deltaTime)
    {
        HandleMovement();

        CollisionObject.Position = cachedTransform.position;

        timer += GameTime.DeltaTime;

        if (timer >= lifeTime)
        {
            if (!IsReleased)
            {
                ObjectPoolManager.Release(gameObject);
                IsReleased = true;
            }
        }
    }

    protected abstract void HandleMovement();

    public void OnCollision(CollisionObject other)
    {
        DamageContext damageContext = DamageResolver.CreateDamageContext(
            AttackContext,
            other.Entity,
            cachedTransform.position, 
            (other.Entity.CachedTransform.position - cachedTransform.position).normalized);

        DamageResolver.ProcessHit(damageContext);

        if(!IsReleased)
        {
            CollisionObject.Active = false;
            ObjectPoolManager.Release(gameObject);
            IsReleased = true;
        }
    }

    public virtual void OnSpawn()
    {
        timer = 0;
        IsReleased = false;
        CollisionObject.Active = true;
        CollisionObject.Position = cachedTransform.position;

        GameManager.RegisterCollisionObject(CollisionObject);
        ProjectileSystem.Register(this);
    }

    public virtual void OnDespawn()
    {
        CollisionObject.Active = false;
        GameManager.UnregisterCollisionObject(CollisionObject);
        ProjectileSystem.MarkForRemoval(this);
    }


    private void OnDrawGizmos()
    {
        if(CollisionObject == null) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(cachedTransform.position, cachedTransform.localScale.x);
    }
}
