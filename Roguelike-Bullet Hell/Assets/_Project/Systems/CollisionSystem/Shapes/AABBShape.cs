using System.Collections;
using System.Collections.Generic;
using Collision;
using UnityEngine;

//public class AABBShape : CollisionShape
//{
//    public Vector3 HalfExtents;

//    public override bool Overlaps(CollisionShape other, Vector3 aPos, Vector3 bPos)
//    {
//        if (other is AABBShape box)
//        {
//            return
//                Mathf.Abs(aPos.x - bPos.x) <= (HalfExtents.x + box.HalfExtents.x) &&
//                Mathf.Abs(aPos.y - bPos.y) <= (HalfExtents.y + box.HalfExtents.y) &&
//                Mathf.Abs(aPos.z - bPos.z) <= (HalfExtents.z + box.HalfExtents.z);
//        }



//        return other.Overlaps(this, bPos, aPos);
//    }
//}
