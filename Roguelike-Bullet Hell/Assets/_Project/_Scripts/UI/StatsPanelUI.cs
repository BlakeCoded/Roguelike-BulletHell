using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.UI
{
    public class StatsPanelUI : MonoBehaviour
    {
        [SerializeField] private StatLabelUI[] labels;

        public void Initialize(StatsComponent stats)
        {
            foreach (var label in labels)
            {
                Stat stat = stats.GetStat(label.StatType);

                if(stat != null)
                {
                    label.Initalize(stat);
                    continue;
                }

                label.gameObject.SetActive(false);
            }
        }
    }
}