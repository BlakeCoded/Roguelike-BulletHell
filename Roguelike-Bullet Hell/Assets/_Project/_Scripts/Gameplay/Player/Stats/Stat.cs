using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float baseValue;
        [SerializeField] private float minValue;
        
        private readonly List<StatModifier> modifiers = new();

        private float totalValue;

        private bool isDirty = true;

        public float Value
        {
            get
            {
                if(isDirty)
                {
                    RecalculateValue();
                }

                return totalValue;
            }
        }

        private void RecalculateValue()
        {
            float value = baseValue;

            float percentAdd = 0f;

            foreach (StatModifier modifier in modifiers)
            {
                switch (modifier.Type)
                {
                    case StatModifierType.Flat:
                        value += modifier.Value;
                        break;

                    case StatModifierType.AddativePercent:
                        percentAdd += modifier.Value;
                        break;
                }
            }

            value *= 1 + percentAdd;

            totalValue = Mathf.Max(minValue, value);

            isDirty = false;
        }

        public Stat(float baseValue, float minValue)
        {
            this.baseValue = baseValue;
            this.minValue = minValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);

            isDirty = true;
        }

        public void RemoveModifier(StatModifier modifier)
        {
            modifiers.Remove(modifier);

            isDirty = true;
        }

        public void RemoveAllFromSource(object source)
        {
            modifiers.RemoveAll(modifier => modifier.Source == source);

            isDirty = true;
        }
    }
}