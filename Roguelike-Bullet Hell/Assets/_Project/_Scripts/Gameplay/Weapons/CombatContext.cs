using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct CombatContext
    {
        public CombatEntity Owner { get; }
        public CombatEntity Target { get; }

        public CombatContext(
            CombatEntity Owner,
            CombatEntity Target)
        {
            this.Owner = Owner;
            this.Target = Target;
        }
    }
}