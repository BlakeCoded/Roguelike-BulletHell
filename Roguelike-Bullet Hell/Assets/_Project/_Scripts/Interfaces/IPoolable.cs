using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }
}