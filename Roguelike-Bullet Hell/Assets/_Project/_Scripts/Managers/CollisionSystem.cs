using System.Collections;
using System.Collections.Generic;
using Collision;
using Interfaces;
using Project.Singleton;
using UnityEngine;

public class CollisionSystem : MonoBehaviourSingleton<CollisionSystem>, IBootstrap, ITickable
{
    public const float CELLSIZE = 3f;
    private CollisionWorld CollisionWorld { get; set; }

    public void Initialize()
    {
        OnInternalBootstrap();
    }

    protected override void OnInternalBootstrap()
    {
        base.OnInternalBootstrap();

        CollisionWorld = new CollisionWorld(CELLSIZE);
    }

    public void Tick(float deltaTime)
    {
        CollisionWorld.Tick(deltaTime);
    }

    private void Update()
    {
        Tick(GameTime.DeltaTime);
    }

    public static bool Raycast(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out RaycastHitData hit)
    {
        if (IsQuitting)
        {
            hit = default;
            return false;
        }

        return Instance.CollisionWorld.Raycast(origin, direction.normalized, distance, collisionLayer, out hit);
    }

    public static bool RaycastAll(Vector3 origin, Vector3 direction, float distance, CollisionLayer collisionLayer, out List<RaycastHitData> hits)
    {
        if (IsQuitting)
        {
            hits = default;
            return false;
        }

        return Instance.CollisionWorld.RaycastAll(origin, direction.normalized, distance, collisionLayer, out hits);
    }

    public static void RegisterCollisionObject(CollisionObject obj)
    {
        Instance.InternalRegisterCollisionObject(obj);
    }

    public static void UnregisterCollisionObject(CollisionObject obj)
    {
        if (IsQuitting) return;

        Instance.InternalUnregisterCollisionObject(obj);
    }

    public static void EnableCollisionObject(CollisionObject obj)
    {
        Instance.InternalEnableCollisionObject(obj);
    }

    public static void DisableCollisionObject(CollisionObject obj)
    {
        Instance.InternalDisableCollisionObject(obj);
    }

    private void InternalRegisterCollisionObject(CollisionObject obj)
    {
        CollisionWorld.Register(obj);
    }

    private void InternalUnregisterCollisionObject(CollisionObject obj)
    {
        CollisionWorld.Unregister(obj);
    }

    private void InternalEnableCollisionObject(CollisionObject obj)
    {
        CollisionWorld.Enable(obj);
    }

    private void InternalDisableCollisionObject(CollisionObject obj)
    {
        CollisionWorld.Disable(obj);
    }
}