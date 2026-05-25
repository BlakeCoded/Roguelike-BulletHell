using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float baseValue;
        
        private readonly List<StatModifier> modifiers = new();

        private float totalValue;

        private bool isDirty = true;

        public Action OnStatChanged;

        public float Value
        {
            get
            {
                if(isDirty)
                {
                    RecalculateValue();
                    OnStatChanged?.Invoke();
                }

                return totalValue;
            }
        }

        private void RecalculateValue()
        {
            float value = baseValue;

            float percentAdd = 0f;

            float multiplier = 1f;

            foreach (StatModifier modifier in modifiers)
            {
                switch (modifier.Type)
                {
                    case StatModifierType.Flat:
                        value += modifier.Value;
                        break;

                    case StatModifierType.AdditivePercent:
                        percentAdd += modifier.Value;
                        break;

                    case StatModifierType.Multiplyer:
                        multiplier += modifier.Value;
                        break;

                }
            }

            value *= 1 + percentAdd;

            value *= multiplier;

            totalValue = value;

            isDirty = false;
        }

        public Stat(float baseValue)
        {
            this.baseValue = baseValue;
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

        public void SetBaseValue(float value)
        {
            baseValue = value;
        }
    }
}