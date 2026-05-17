using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IProjectile
    {
        float MoveSpeed { get; }
        float Damage { get; }
        float Lifetime { get; }

        void Initialize(float damage, float moveSpeed, float lifeTime);
    }
}