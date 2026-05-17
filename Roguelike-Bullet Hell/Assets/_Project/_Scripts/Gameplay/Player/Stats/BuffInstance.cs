using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    public class BuffInstance
    {
        public BuffData Data;

        public float RemainingDuration;

        public BuffInstance(BuffData data)
        {
            Data = data;

            RemainingDuration = data.Durration;
        }
    }
}