using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Gameplay.Stats;
using UnityEngine.UI;

namespace Project.Gameplay.Buffs
{
    [CreateAssetMenu(menuName = "Buffs/Buff")]
    public class BuffData : ScriptableObject
    {
        public string BuffName;
        public Image Icon;
        public float Durration;

        public List<StatModifierData> Modifiers;
    }
}