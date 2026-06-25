using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Collision
{
    public class GridRayTraversal
    {
        public Vector3Int CurrentCell => currentCell;
        public float DistanceTravelled { get; private set; }

        private Vector3Int currentCell;

        private float cellSize;

        private int stepX;
        private int stepY;
        private int stepZ;

        private float tMaxX;
        private float tMaxY;
        private float tMaxZ;

        private float tDeltaX;
        private float tDeltaY;
        private float tDeltaZ;

        public GridRayTraversal(Vector3 origin, Vector3 direction, float cellSize)
        {
            this.cellSize = cellSize;

            currentCell = WorldToCell(origin);

            stepX = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0;
            stepY = direction.y > 0 ? 1 : direction.y < 0 ? -1 : 0;
            stepZ = direction.z > 0 ? 1 : direction.z < 0 ? -1 : 0;

            // X Axis
            if (stepX != 0)
            {
                float nextBoundaryX = stepX > 0 ? (currentCell.x + 1) * cellSize : currentCell.x * cellSize;

                tMaxX = Mathf.Abs((nextBoundaryX - origin.x) / direction.x);
                tDeltaX = cellSize / Mathf.Abs(direction.x);
            }
            else
            {
                tMaxX = float.PositiveInfinity;
                tDeltaX = float.PositiveInfinity;
            }

            // Y Axis
            if (stepY != 0)
            {
                float nextBoundaryY = stepY > 0 ? (currentCell.y + 1) * cellSize : currentCell.y * cellSize;

                tMaxY = Mathf.Abs((nextBoundaryY - origin.y) / direction.y);
                tDeltaY = cellSize / Mathf.Abs(direction.y);
            }
            else
            {
                tMaxY = float.PositiveInfinity;
                tDeltaY = float.PositiveInfinity;
            }

            // Z Axis
            if (stepZ != 0)
            {
                float nextBoundaryZ = stepZ > 0 ? (currentCell.z + 1) * cellSize : currentCell.z * cellSize;

                tMaxZ = Mathf.Abs((nextBoundaryZ - origin.z) / direction.z);
                tDeltaZ = cellSize / Mathf.Abs(direction.z);
            }
            else
            {
                tMaxZ = float.PositiveInfinity;
                tDeltaZ = float.PositiveInfinity;
            }
        }

        public void Step()
        {
            if(tMaxX <= tMaxY && tMaxX <= tMaxZ)
            {
                currentCell.x += stepX;
                DistanceTravelled = tMaxX;
                tMaxX += tDeltaX;
            }
            else if(tMaxY < tMaxZ)
            {
                currentCell.y += stepY;
                DistanceTravelled = tMaxY;
                tMaxY += tDeltaY;
            }
            else
            {
                currentCell.z += stepZ;
                DistanceTravelled = tMaxZ;
                tMaxZ += tDeltaZ;
            }
        }

        private Vector3Int WorldToCell(Vector3 position)
        {
            return new Vector3Int(
                Mathf.FloorToInt(position.x / cellSize),
                Mathf.FloorToInt(position.y / cellSize),
                Mathf.FloorToInt(position.z / cellSize));
        }
    }
}