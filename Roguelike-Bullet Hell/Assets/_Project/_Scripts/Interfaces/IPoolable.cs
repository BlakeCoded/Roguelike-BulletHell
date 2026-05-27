using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        bool IsReleased { get; }
        void OnSpawn();
        void OnDespawn();
    }
}