using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public struct RaycastHitData
    {
        public CollisionObject CollisionObject;
        public Vector3 HitPoint;
        public float HitDistance;
    }
}