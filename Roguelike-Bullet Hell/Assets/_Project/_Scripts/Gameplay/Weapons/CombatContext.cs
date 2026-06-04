using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct CombatContext
    {
        public Entity Owner { get; }
        public Entity Target { get; }

        public CombatContext(
            Entity Owner,
            Entity Target)
        {
            this.Owner = Owner;
            this.Target = Target;
        }
    }
}