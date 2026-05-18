using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;

namespace Project.Gameplay.Buffs
{
    [RequireComponent(typeof(StatsComponent))]
    public class BuffComponent : MonoBehaviour
    {
        private List<BuffInstance> activeBuffs = new();

        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();
        }

        public void AddBuff(BuffData buff)
        {
            BuffInstance buffInstance = new(buff);

            activeBuffs.Add(buffInstance);

            foreach (StatModifierData mod in buff.Modifiers)
            {
                stats.AddStatModifier(mod.StatType, new StatModifier(mod.ModifierType, mod.Value, buffInstance));
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

                if(buff.HasDuration == false) continue;

                buff.RemainingDuration -= Time.deltaTime;

                if (buff.RemainingDuration <= 0)
                {
                    RemoveBuff(buff);
                }
            }
        }
    }
}