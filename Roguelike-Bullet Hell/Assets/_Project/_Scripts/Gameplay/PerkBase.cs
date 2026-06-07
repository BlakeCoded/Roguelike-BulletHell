using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;
using Project.Gameplay.Combat;

public class PerkBase : MonoBehaviour
{
    [SerializeField] private StatModifierData modifier;

    private void OnTriggerEnter(Collider other)
    {
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

    private void AddOnHitEffect(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox player))
        {
            if(player.Owner.TryGetComponent<PlayerWeapons>(out PlayerWeapons playerWeapons))
            {
                playerWeapons.AddOnHitEffect(new BurnOnHitEffect());
                //playerWeapons.AddOnHitEffect(new DamageOnHitEffect());
            }
        }
    }
}
