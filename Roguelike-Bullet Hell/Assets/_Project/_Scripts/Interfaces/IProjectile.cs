using System.Collections;
using System.Collections.Generic;
using Collision;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Interfaces
{
    public interface IProjectile
    {
        int Index { get; set; }
        CollisionObject CollisionObject { get; }
        AttackContext AttackContext { get; }
        void Initialize(AttackContext context, float projectileSpeed, float projectileSize);
        virtual void Tick(float deltaTime) { }
    }
}