using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Interfaces
{
    public interface IProjectile
    {
        AttackContext AttackContext { get; }

        void Initialize(AttackContext context);
    }
}