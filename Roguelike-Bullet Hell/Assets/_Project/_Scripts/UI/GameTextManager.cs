using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;
using Project.UI;
using UnityEngine;

public class GameTextManager : MonoBehaviour
{
    public static GameTextManager Instance;

    [SerializeField] DamageTextUI prefab;
    [SerializeField] Canvas canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public void ShowDamage(Vector3 worldPosition, DamageContext context)
    {
        DamageTextUI text = PoolManager.Instance.Get(prefab, Vector3.zero, Quaternion.identity, canvas.transform);

        text.Initialize(worldPosition, context.Damage, context.IsCrit);
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }
}
