using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IOnHitEffect
    {
        void OnHit(IDamageable target);
    }
}