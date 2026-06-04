using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Gameplay.Stats
{
    [System.Serializable]
    public class Stat : IReadOnlyStat
    {
        // Public
        public event Action<float> OnValueChanged;

        public float Value
        {
            get
            {
                if(isDirty)
                {
                    RecalculateValue();
                    OnValueChanged?.Invoke(Value);
                }

                return totalValue;
            }
        }

        // Private
        [SerializeField] private float baseValue;
        private float totalValue;
        private bool isDirty = true;
        private readonly List<StatModifier> modifiers = new();

        private void RecalculateValue()
        {
            float value = this.baseValue;

            float percentAdd = 0f;

            float multiplier = 1f;

            foreach (StatModifier modifier in this.modifiers)
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

            this.totalValue = value;

            this.isDirty = false;
        }

        public Stat(float baseValue)
        {
            SetBaseValue(baseValue);
        }

        public void AddModifier(StatModifier modifier)
        {
            this.modifiers.Add(modifier);

            this.isDirty = true;
        }

        public void RemoveModifier(StatModifier modifier)
        {
            this.modifiers.Remove(modifier);

            this.isDirty = true;
        }

        public void RemoveAllFromSource(object source)
        {
            this.modifiers.RemoveAll(modifier => modifier.Source == source);

            this.isDirty = true;
        }

        public void SetBaseValue(float value)
        {
            this.baseValue = value;

            this.isDirty = true;
        }
    }
}