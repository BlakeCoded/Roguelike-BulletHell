using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public struct CollisionEvent
    {
        public readonly CollisionObject A;
        public readonly CollisionObject B;

        public CollisionEvent(CollisionObject a, CollisionObject b)
        {
            A = a;
            B = b;
        }
    }
}