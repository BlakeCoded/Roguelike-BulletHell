using System.Collections;
using System.Collections.Generic;
using Collision;
using UnityEngine;

namespace Interfaces
{
    public interface ICollisionHandler
    {
        void OnHit(CollisionObject other);
    }
}