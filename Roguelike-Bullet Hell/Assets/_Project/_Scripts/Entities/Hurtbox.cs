using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public class Hurtbox : MonoBehaviour
    {
        public CombatEntity Owner { get; private set; }

        private void Awake()
        {
            Owner = GetComponentInParent<CombatEntity>();
        }
    }
}