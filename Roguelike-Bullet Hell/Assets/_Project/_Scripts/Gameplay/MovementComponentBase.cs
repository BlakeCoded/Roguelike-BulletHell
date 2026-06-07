using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Movement
{
    public abstract class MovementComponentBase : MonoBehaviour
    {
        public bool CanMove { get; set; } = true;
        public float MoveSpeed => stats.GetStatValue(StatType.MoveSpeed);

        protected StatsComponent stats;

        protected virtual void Awake()
        {
            stats = GetComponent<StatsComponent>();
        }

        public virtual void Move(Vector3 direction) { }

        public abstract void TickMovement();
    }
}