using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class StatusEffectDefinition : ScriptableObject
    {
        public float TickRate;
        public float Duration;
        public float DamagePerTick;
    }
}