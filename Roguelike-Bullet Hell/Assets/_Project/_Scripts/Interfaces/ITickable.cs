using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface ITickable
    {
        void Tick(float deltaTime);
    }
}