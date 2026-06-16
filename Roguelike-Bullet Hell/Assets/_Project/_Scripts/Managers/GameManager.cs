using Interfaces;
using Project.Gameplay.UI;
using Project.Singleton;
using Project.UI;
using Project.Systems.Keybinds;
using UnityEngine;
using Project.Player;

public class GameManager : MonoBehaviourSingleton<GameManager>, IInitializable
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] StatsPanelUI statsPanel;
    [SerializeField] PlayerTabPanel characterUI;
    [SerializeField] InventoryPanel inventoryPanel;

    PlayerEntity PlayerEntity;

    public bool IsInitialized {  get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();

        Init();
    }

    public void Init() // eventually init core systems and assign everything in start.
    {
        IsInitialized = true;

        GameObject player = Instantiate(playerPrefab, new Vector3(0,1,0), Quaternion.identity);

        PlayerEntity = player.GetComponent<PlayerEntity>();

        statsPanel.Initialize(PlayerEntity.Stats);

        inventoryPanel.Init(PlayerEntity);
    }

    private void Start()
    {
        InputManager.Subscribe(characterUI.HandleInput);
    }

    private void OnDisable()
    {
        InputManager.Unsubscribe(characterUI.HandleInput);
    }
}