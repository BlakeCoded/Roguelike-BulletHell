using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    [CreateAssetMenu(menuName = "Buffs/Buff")]
    public class BuffData : ScriptableObject
    {
        public string BuffName;

        public float Durration;

        public List<StatModifierData> Modifiers;
    }
}