using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Player
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {
        public bool CanMove { get; set; }

        private StatsComponent stats;

        private void Awake()
        {
            stats = GetComponent<StatsComponent>();

            CanMove = true;
        }

        public void Move(Vector2 direction)
        {
            if (CanMove == false || direction == Vector2.zero) return;

            transform.MoveByXZ(direction * stats.GetStatValue(StatType.MoveSpeed) * Time.deltaTime);
        }
    }
}