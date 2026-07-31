using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Combat;
using Project.Gameplay.Pooling;
using Project.Singleton;
using Project.UI;
using Unity.VisualScripting;
using UnityEngine;

public class GameTextManager : MonoBehaviourSingleton<GameTextManager>, IBootstrap, ITickable
{
    [SerializeField] DamageTextUI prefab;
    [SerializeField] Canvas canvas;
    private static Canvas Canvas { get; set; }

    private static readonly List<DamageTextUI> damageTextUIs = new();

    private static readonly List<DamageTextUI> pendingRemovals = new();

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        OnInternalBootstrap();

        RegisiterCanvas(canvas);
    }

    protected override void OnInternalBootstrap()
    {
        base.OnInternalBootstrap();
    }

    private void Update()
    {
        Tick(GameTime.DeltaTime);
    }

    public void Tick(float deltaTime)
    {
        for (int i = damageTextUIs.Count - 1; i >= 0; i--)
        {
            damageTextUIs[i].Tick(deltaTime);
        }

        for (int i = 0; i < pendingRemovals.Count; i++)
        {
            Unregister(pendingRemovals[i]);
        }

        pendingRemovals.Clear();
    }

    private void ShowDamage(DamageResult result, Vector3 hitPosition)
    {
        DamageTextUI damageTextUI = ObjectPoolManager.GetUI<DamageTextUI>(prefab, Canvas.transform);

        damageTextUI.Initialize(result, hitPosition);

        Register(damageTextUI);
    }

    private static void Register(DamageTextUI text)
    {
        text.SetIndex(damageTextUIs.Count);
        damageTextUIs.Add(text);
    }

    private static void Unregister(DamageTextUI text)
    {
        int index = text.Index;
        int lastIndex = damageTextUIs.Count - 1;

        if (index < 0 || index > lastIndex)
        {
            damageTextUIs.RemoveAt(lastIndex);
            return;
        }

        if(index != lastIndex)
        {
            DamageTextUI last = damageTextUIs[lastIndex];

            damageTextUIs[index] = last;
            last.SetIndex(index);
        }

        damageTextUIs.RemoveAt(lastIndex);
        text.SetIndex(-1);
    }

    public static void MarkForRemoval(DamageTextUI text)
    {
        pendingRemovals.Add(text);
    }

    public static void RegisiterCanvas(Canvas canvas)
    {
        Canvas = canvas;
    }


    private void OnEnable()
    {
        CombatEvents.OnDamageDealt += ShowDamage;
    }

    private void OnDisable()
    {
        CombatEvents.OnDamageDealt -= ShowDamage;
    }
}