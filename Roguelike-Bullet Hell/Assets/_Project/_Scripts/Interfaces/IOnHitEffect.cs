using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Combat;
using UnityEngine;

namespace Interfaces
{
    public interface IOnHitEffect
    {
        public string Id { get; }
        public StackRule StackRule { get; }
        void Apply(DamageResult result, int count);
    }
}