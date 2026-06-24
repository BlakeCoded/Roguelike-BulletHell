using System.Collections;
using System.Collections.Generic;
using System.Text;
using Interfaces;
using Project.Gameplay.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    public class StatLabelUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private StatType statType;
        public StatType StatType => statType;

        private IReadOnlyStat stat;

        public void Initalize(IReadOnlyStat stat)
        {
            this.stat = stat;

            UpdateValue(stat.Value);

            stat.OnValueChanged += UpdateValue;
        }

        private void UpdateValue(float value)
        {
            valueText.text = $"{statType}: {value:0.##}";
        }

        private void OnDestroy()
        {
            if(stat != null)
            {
                stat.OnValueChanged -= UpdateValue;
            }
        }
    }
}