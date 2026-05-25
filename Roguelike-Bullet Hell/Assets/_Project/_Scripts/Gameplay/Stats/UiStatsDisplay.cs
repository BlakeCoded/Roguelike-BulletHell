using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Gameplay.Stats;
using TMPro;
using UnityEngine;

namespace Project.Gameplay.UI
{
    public class UiStatsDisplay : MonoBehaviour
    {
        [SerializeField] StatsComponent playerStats;
        [SerializeField] TextMeshProUGUI statUI;

        public void Refresh()
        {
            statUI.text = "Damage: " + playerStats.GetStatValue(StatType.Damage).ToString("0");
        }

        private void Start()
        {
            playerStats.GetStat(StatType.Damage).OnStatChanged += Refresh;
            Refresh();
        }

        private void OnEnable()
        {
            //playerStats.GetStat(StatType.Damage).OnStatChanged += Refresh;
        }

        private void OnDisable()
        {
            playerStats.GetStat(StatType.Damage).OnStatChanged -= Refresh;
        }
    }
}