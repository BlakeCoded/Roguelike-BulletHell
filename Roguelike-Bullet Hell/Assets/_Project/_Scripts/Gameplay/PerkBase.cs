using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;
using Project.Gameplay.Buffs;
using Project.Gameplay.Combat;

public class PerkBase : MonoBehaviour
{
    [SerializeField] private StatModifierData modifier;
    [SerializeField] private List<BuffData> buffs;

    private void OnTriggerEnter(Collider other)
    {
        //AddStat(other);

        //AddBuffs(other);

        AddOnHitEffect(other);
    }

    private void AddStat(Collider other)
    {
        Hurtbox player = other.GetComponent<Hurtbox>();

        if (player.Owner.Stats != null)
        {
            Debug.Log(player.Owner.Stats.GetStatValue(StatType.AttackSpeed));
            player.Owner.Stats.AddStatModifier(modifier.StatType, new StatModifier(modifier.ModifierType, modifier.Value));
            Debug.Log(player.Owner.Stats.GetStatValue(StatType.AttackSpeed));
        }
    }

    private void AddBuffs(Collider other)
    {
        BuffComponent bc = other.GetComponentInParent<BuffComponent>();

        foreach (BuffData buff in buffs)
        {
            bc.AddBuff(buff);
        }
    }

    private void AddOnHitEffect(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox player))
        {
            if(player.Owner.TryGetComponent<PlayerWeapons>(out PlayerWeapons playerWeapons))
            {
                playerWeapons.AddOnHitEffect(new BurnOnHitEffect());
            }
        }
    }
}
