using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public readonly struct DamageResult
    {
        public CombatContext CombatContext { get; }
        public float PreModifierDamage { get; }
        public float DamageDealt { get; }
        public bool IsCritical { get; }
        public bool KilledTarget { get; }

        public DamageResult(
            CombatContext CombatContext,
            float PreModifierDamage,
            float DamageDealt,
            bool IsCritical,
            bool KilledTarget)
        {
            this.CombatContext = CombatContext;
            this.PreModifierDamage = PreModifierDamage;
            this.DamageDealt = DamageDealt;
            this.IsCritical = IsCritical;
            this.KilledTarget = KilledTarget;
        }
    }
}