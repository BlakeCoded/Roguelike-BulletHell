using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;
using Project.UI;
using UnityEngine;
using Project.Singleton;

public class GameTextManager : MonoBehaviourSingleton<GameTextManager>
{
    [SerializeField] DamageTextUI prefab;
    [SerializeField] Canvas canvas;

    public void ShowDamage(DamageResult result, Vector3 hitPosition)
    {
        DamageTextUI text = PoolManager.Instance.Get(prefab, Vector3.zero, Quaternion.identity, canvas.transform);

        text.Initialize(result, hitPosition);
    }
}
