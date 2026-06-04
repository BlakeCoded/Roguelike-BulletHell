using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct DamageResult
    {
        public CombatContext CombatContext { get; }
        public float DamageDealt { get; }
        public bool IsCritical { get; }
        public bool KilledTarget { get; }

        public DamageResult(
            CombatContext CombatContext,
            float DamageDealt,
            bool IsCritical,
            bool KilledTarget)
        {
            this.CombatContext = CombatContext;
            this.DamageDealt = DamageDealt;
            this.IsCritical = IsCritical;
            this.KilledTarget = KilledTarget;
        }
    }
}