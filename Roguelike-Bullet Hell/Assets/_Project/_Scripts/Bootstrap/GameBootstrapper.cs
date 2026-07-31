using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

/// <summary>
/// Responsible for creating systems that survive scene loads.
/// </summary>
public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] bootManagers;

    private void Awake()
    {
        foreach (IBootstrap manager in bootManagers)
        {
            manager.Initialize();
        }
    }
}