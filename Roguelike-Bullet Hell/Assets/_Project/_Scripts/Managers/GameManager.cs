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

    protected override void OnAwake()
    {
        base.OnAwake();

        Initialize();
    }

    public void Initialize()
    {
        foreach (var manager in bootManagers)
        {
            if (manager is IBootstrap bootstrap)
            {
                bootstrap.Initialize();
            }
        }
    }

    private void Update()
    {
        ProjectileSystem.Tick(GameTime.DeltaTime);
    }
}