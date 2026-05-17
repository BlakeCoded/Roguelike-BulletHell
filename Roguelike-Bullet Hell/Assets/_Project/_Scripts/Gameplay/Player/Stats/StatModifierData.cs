using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    [System.Serializable]
    public class StatModifierData
    {
        public StatType StatType;

        public float Value;

        public StatModifierType ModifierType;
    }
}