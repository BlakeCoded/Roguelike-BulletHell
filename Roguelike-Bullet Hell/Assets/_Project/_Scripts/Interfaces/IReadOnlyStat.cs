using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IReadOnlyStat
    {
        float Value { get; }
        event Action<float> OnValueChanged;
    }
}