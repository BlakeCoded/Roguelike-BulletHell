using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using System;
using UnityEngine;

/// <summary>
/// Centralized event dispatcher for combat-related notifications.
/// </summary>
public static class CombatEvents
{
    #region ON_DAMAGE_DEALT
    public static event Action<DamageResult, Vector3> OnDamageDealt;

    public static void RaiseDamageDealt(
        DamageResult result,
        Vector3 hitPosition)
    {
        OnDamageDealt?.Invoke(result, hitPosition);
    }
    #endregion
}