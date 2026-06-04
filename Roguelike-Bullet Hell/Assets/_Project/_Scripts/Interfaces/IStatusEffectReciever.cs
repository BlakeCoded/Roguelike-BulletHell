using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Interfaces
{
    public interface IStatusEffectReciver
    {
        void AddStatusEffect(StatusEffect effect);
    }
}
