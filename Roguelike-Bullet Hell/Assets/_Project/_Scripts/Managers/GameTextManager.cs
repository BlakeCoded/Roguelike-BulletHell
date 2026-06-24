using Project.Gameplay.Pooling;
using Project.Gameplay.Combat;
using Project.UI;
using UnityEngine;
using Project.Singleton;
using System.Collections.Generic;

public class GameTextManager : MonoBehaviourSingleton<GameTextManager>
{
    [SerializeField] DamageTextUI prefab;
    [SerializeField] Canvas canvas;

    //private Dictionary<CombatEntity, DamageTextUI> ActiveDamage = new();

    //public void RemoveDamageTextUI(CombatEntity entity)
    //{
    //    if(ActiveDamage.ContainsKey(entity))
    //    {
    //        ActiveDamage.Remove(entity);
    //    }
    //}

    //private void ShowDamage(DamageResult result, Vector3 hitPosition)
    //{
    //    if(!ActiveDamage.TryGetValue(result.CombatContext.Target, out DamageTextUI damageTextUI))
    //    {
    //        damageTextUI = ObjectPoolManager.GetUI<DamageTextUI>(prefab, canvas.transform);

    //        damageTextUI.Initialize(result, hitPosition);
    //    }

    //    damageTextUI.UpdateDamageNumber(result.DamageDealt);
    //}

    private void ShowDamage(DamageResult result, Vector3 hitPosition)
    {
        DamageTextUI damageTextUI = ObjectPoolManager.GetUI<DamageTextUI>(prefab, canvas.transform);

        damageTextUI.Initialize(result, hitPosition);
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