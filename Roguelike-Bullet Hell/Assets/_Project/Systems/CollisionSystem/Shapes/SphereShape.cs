using System.Collections;
using System.Collections.Generic;
using Collision;
using UnityEngine;

//public class SphereShape : CollisionShape
//{
//    public float Radius;

//    public override bool Overlaps(CollisionShape other, Vector3 aPos, Vector3 bPos)
//    {
//        if(other is  SphereShape sphere)
//        {
//            float r = Radius + sphere.Radius;
//            return (aPos - bPos).sqrMagnitude <= r * r;
//        }

//        return other.Overlaps(this, bPos, aPos);
//    }
//}
