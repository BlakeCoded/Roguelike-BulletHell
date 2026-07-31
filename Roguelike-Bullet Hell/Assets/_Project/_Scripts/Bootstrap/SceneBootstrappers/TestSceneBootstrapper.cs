using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.UI;
using Project.Gameplay.UI;
using Project.Player;

public class TestSceneBootstrapper : SceneBootstrapper
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] StatsPanelUI statsPanel;
    [SerializeField] InventoryPanel inventoryPanel;

    PlayerEntity playerEntity;

    private void Start()
    {
        Initialise();
    }

    public void Initialise()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);

        playerEntity = player.GetComponent<PlayerEntity>();

        statsPanel.Initialize(playerEntity.Stats);

        inventoryPanel.Init(playerEntity);

        CursorManager.Lock();
    }
}