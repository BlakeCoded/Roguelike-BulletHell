using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    public class StatModifier
    {
        public float Value;
        public StatModifierType Type;
        public object Source;

        public StatModifier(float value, StatModifierType type, object source = null)
        {
            Value = value;
            Type = type;
            Source = source;
        }
    }
}