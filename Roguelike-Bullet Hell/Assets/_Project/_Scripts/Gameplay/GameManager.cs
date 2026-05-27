using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using Project.UI;
using UnityEngine;

public class GameManager : MonoBehaviour, IInitializable
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] StatsPanelUI statsPanel;

    public bool IsInitialized {  get; private set; }

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        IsInitialized = true;

        GameObject player = Instantiate(playerPrefab, new Vector3(0,1,0), Quaternion.identity);

        statsPanel.Initialize(player.GetComponent<StatsComponent>());
    }
}
