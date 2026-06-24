using System.Collections;
using System.Collections.Generic;
using Project.Gameplay.Movement;
using Project.Gameplay.Stats;
using UnityEngine;

namespace Project.Gameplay.Movement
{
    public class NpcMovement : MovementComponentBase
    {
        public override bool CanMove { get; set; }
        public override float MoveSpeed => Stats.GetStatValue(StatType.MoveSpeed);

        public override void TickMovement()
        {

        }
    }
}