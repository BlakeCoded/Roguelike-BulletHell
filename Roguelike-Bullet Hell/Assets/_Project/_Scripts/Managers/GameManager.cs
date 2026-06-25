using Interfaces;
using Project.Gameplay.UI;
using Project.Singleton;
using Project.UI;
using UnityEngine;
using Project.Player;
using Collision;
using System.Collections.Generic;

/// <summary>
/// Central game coordinator responsible for initializing and managing
/// global game systems during startup and runtime.
/// </summary>
public class GameManager : MonoBehaviourSingleton<GameManager>, IBootstrap
{
    [Header("BOOTSTRAP_MANAGERS")]
    [SerializeField] MonoBehaviour[] bootManagers;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] StatsPanelUI statsPanel;
    [SerializeField] InventoryPanel inventoryPanel;

    PlayerEntity PlayerEntity;

    public CollisionSystem CollisionSystem { get; private set; }
    private const float cellSize = 3f;

    protected override void OnAwake()
    {
        base.OnAwake();

        Initialize();
    }

    public void Initialize()
    {
        CollisionSystem = new CollisionSystem(cellSize);

        foreach (var manager in bootManagers)
        {
            if (manager is IBootstrap bootstrap)
            {
                bootstrap.Initialize();
            }
        }
    }

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);

        PlayerEntity = player.GetComponent<PlayerEntity>();

        statsPanel.Initialize(PlayerEntity.Stats);

        inventoryPanel.Init(PlayerEntity);

        CursorManager.Lock();
    }

    private void Update()
    {
        ProjectileSystem.Tick(GameTime.DeltaTime);
        CollisionSystem.Tick();
    }

    #region COLLISION_SYSTEM

    public static bool Raycast(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out RaycastHitData hit)
    {
        if (IsQuitting)
        {
            hit = default;
            return false;
        }

        return Instance.CollisionSystem.Raycast(origin, direction.normalized, distance, collisionLayer, out hit);
    }

    public static bool RaycastAll(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out List<RaycastHitData> hits)
    {
        if (IsQuitting)
        {
            hits = default;
            return false;
        }

        return Instance.CollisionSystem.RaycastAll(origin, direction.normalized, distance, collisionLayer, out hits);
    }

    public static void RegisterCollisionObject(CollisionObject obj)
    {
        Instance.InternalRegisterCollisionObject(obj);
    }
    public static void UnregisterCollisionObject(CollisionObject obj)
    {
        Instance.InternalUnregisterCollisionObject(obj);
    }

    private void InternalRegisterCollisionObject(CollisionObject obj)
    {
        CollisionSystem.Register(obj);
    }

    private void InternalUnregisterCollisionObject(CollisionObject obj)
    {
        CollisionSystem.Unregister(obj);
    }

    #endregion COLLISION_SYSTEM
}