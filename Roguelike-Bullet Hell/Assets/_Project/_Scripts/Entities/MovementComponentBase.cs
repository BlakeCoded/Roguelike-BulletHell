using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Movement
{
    public abstract class MovementComponentBase : MonoBehaviour
    {
        public abstract bool CanMove { get; set; }
        public abstract float MoveSpeed { get; }

        protected StatsComponent Stats;

        protected virtual void Awake()
        {
            Stats = GetComponent<StatsComponent>();
        }

        public virtual void Move(Vector3 direction) { }

        public abstract void TickMovement();
    }
}