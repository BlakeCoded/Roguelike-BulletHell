using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public static class CollisionMatrix
    {
        private static readonly int LayerCount = Enum.GetValues(typeof(CollisionLayer)).Length;

        private static bool[,] matrix = new bool[LayerCount, LayerCount];

        static CollisionMatrix()
        {
            // Player Projectiles hits Enemy + World
            Set(CollisionLayer.PlayerProjectiles, CollisionLayer.Enemy, true);
            Set(CollisionLayer.PlayerProjectiles, CollisionLayer.World, true);

            // Enemy Projectiles hits Player + World
            Set(CollisionLayer.EnemyProjectiles, CollisionLayer.Player, true);
            Set(CollisionLayer.EnemyProjectiles, CollisionLayer.World, true);

            // Player hits Enemy + World
            Set(CollisionLayer.Player, CollisionLayer.Enemy, true);
            Set(CollisionLayer.Player, CollisionLayer.World, true);

            // Enemy hits Player + World
            Set(CollisionLayer.Enemy, CollisionLayer.Player, true);
            Set(CollisionLayer.Enemy, CollisionLayer.World, true);
        }

        public static void Set(CollisionLayer a, CollisionLayer b, bool value)
        {
            matrix[(int)a, (int)b] = value;
            matrix[(int)b, (int)a] = value;
        }

        public static bool CanCollide(CollisionLayer a, CollisionLayer b)
        {
            return matrix[(int)a, (int)b];
        }
    }
}