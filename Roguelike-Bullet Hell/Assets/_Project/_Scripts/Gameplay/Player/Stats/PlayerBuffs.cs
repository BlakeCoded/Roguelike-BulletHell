using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Stats;

namespace Player
{
    public class PlayerBuffs : MonoBehaviour
    {
        private List<BuffInstance> activeBuffs = new();

        private PlayerStats stats;

        private void Awake()
        {
            stats = GetComponent<PlayerStats>();
        }

        public void AddBuff(BuffData buff)
        {
            BuffInstance buffInstance = new(buff);

            activeBuffs.Add(buffInstance);

            foreach (StatModifierData mod in buff.Modifiers)
            {
                stats.AddStatModifier(mod.StatType, new StatModifier(mod.Value, mod.ModifierType, buffInstance));
            }
        }

        private void RemoveBuff(BuffInstance buff)
        {
            foreach (StatModifierData mod in buff.Data.Modifiers)
            {
                stats.RemoveAllStatModifiers(mod.StatType, buff);
            }
        }

        private void Update()
        {
            for (int i = activeBuffs.Count - 1; i >= 0; i--)
            {
                BuffInstance buff = activeBuffs[i];

                buff.RemainingDuration -= Time.deltaTime;

                if (buff.RemainingDuration <= 0)
                {
                    RemoveBuff(buff);
                }
            }
        }
    }
}