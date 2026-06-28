using Collision;
using Interfaces;
using Inventory;
using OnHitEffect;
using Project.Gameplay.Combat;
using Project.Gameplay.Health;
using Project.Gameplay.Movement;
using Project.Gameplay.Stats;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(StatsComponent))]
[RequireComponent(typeof(StatusEffectComponent))]
[RequireComponent(typeof(CombatEffectsComponent))]
[RequireComponent(typeof(InventoryComponent))]
public abstract class CombatEntity : MonoBehaviour, ICollisionHandler
{
    // Positioning Data
    public Transform CachedTransform { get; private set; }

    // Health Data
    public HealthComponent Health {  get; private set; }

    // Stat Data
    public StatsComponent Stats { get; private set; }

    // Effects
    public StatusEffectComponent StatusEffects { get; private set; }
    public CombatEffectsComponent CombatEffects { get; private set; }

    // Movement Data
    public MovementComponentBase Movement { get; private set; }

    // Inventory Data
    public InventoryComponent Inventory { get; private set; }

    // Team
    public Team Team => team;
    [SerializeField] private Team team;

    // HitBoxes
    public CollisionObject[] HitBoxes { get; private set; }

    protected virtual void Awake()
    {
        CachedTransform = transform;

        Health = GetComponent<HealthComponent>();

        Stats = GetComponent<StatsComponent>();

        StatusEffects = GetComponent<StatusEffectComponent>();
        CombatEffects = GetComponent<CombatEffectsComponent>();

        Inventory = GetComponent<InventoryComponent>();

        Movement = GetComponent<MovementComponentBase>();

        Debug.Assert(Movement != null, $"{name} requires a MovementComponentBase");

        InitializeCollisionObjects();
        
    }

    protected virtual void Start()
    {
        RegisiterCollisionObjects();
    }

    public virtual void OnHit(CollisionObject other) { }

    private void InitializeCollisionObjects()
    {
        CollisionObjectComponent[] collisionObjects = GetComponentsInChildren<CollisionObjectComponent>();

        HitBoxes = new CollisionObject[collisionObjects.Length];

        for (int i = 0; i < collisionObjects.Length; i++)
        {
            HitBoxes[i] = collisionObjects[i].BuildCollisionObject(this);
        }
    }

    private void RegisiterCollisionObjects()
    {
        foreach (var data in HitBoxes)
        {
            GameManager.RegisterCollisionObject(data);
        }
    }

    protected void SyncCollisionTransform()
    {
        foreach (var obj in HitBoxes)
        {
            obj.Position = obj.Transform.position;
            obj.Rotation = obj.Transform.rotation;
        }
    }
}