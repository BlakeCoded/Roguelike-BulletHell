using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float amount);

        void Heal(float amount);
    }
}