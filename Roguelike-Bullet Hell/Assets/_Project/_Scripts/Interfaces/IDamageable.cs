using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        DamageResult TakeDamage(DamageContext context);
    }
}