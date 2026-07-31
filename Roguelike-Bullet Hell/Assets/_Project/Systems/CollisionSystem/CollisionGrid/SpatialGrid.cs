using System.Collections.Generic;
using UnityEngine;

namespace Collision
{
    public class SpatialGrid // Next optimise occupying cells to remove and add changed cells not all cells.
    {
        private readonly Dictionary<Vector3Int, List<CollisionObject>> cells = new();
        private readonly List<Vector3Int> cellBuffer = new();
        private readonly HashSet<CollisionObject> NoDuplicateObjects = new();

        public readonly float CELLSIZE;

        public SpatialGrid(float cellSize)
        {
            this.CELLSIZE = cellSize;
        }

        public void ResetSpatialGrid()
        {
            NoDuplicateObjects.Clear();
            cellBuffer.Clear();
            cells.Clear();
        }

        public void Add(CollisionObject obj)
        {
            obj.CollisionShape.GetBounds(obj.Position, obj.Rotation, out Vector3 min, out Vector3 max);

            Vector3Int minCell = WorldToCell(min);
            Vector3Int maxCell = WorldToCell(max);

            obj.MinCell = minCell;
            obj.MaxCell = maxCell;

            SetOccupyingCells(obj, minCell, maxCell);
        }

        public void Remove(CollisionObject obj)
        {
            RemoveObjFromOccupiedCells(obj);
        }

        public void UpdateObject(CollisionObject obj)
        {
            obj.CollisionShape.GetBounds(obj.Position, obj.Rotation, out Vector3 min, out Vector3 max);

            Vector3Int minCell = WorldToCell(min);
            Vector3Int maxCell = WorldToCell(max);

            if (obj.MinCell == minCell && obj.MaxCell == maxCell) return;

            RemoveObjFromOccupiedCells(obj);

            SetOccupyingCells(obj, minCell, maxCell);
        }

        public void GetNearby(CollisionObject obj, List<CollisionObject> results)
        {
            results.Clear();
            NoDuplicateObjects.Clear();

            obj.CollisionShape.GetBounds(obj.Position, obj.Rotation, out var min, out var max);

            Vector3Int minCell = WorldToCell(min);
            Vector3Int maxCell = WorldToCell(max);

            for (int x = minCell.x; x <= maxCell.x; x++)
                for (int y = minCell.y; y <= maxCell.y; y++)
                    for (int z = minCell.z; z <= maxCell.z; z++)
                    {
                        Vector3Int cell = new(x, y, z);

                        if (!cells.TryGetValue(cell, out var list))
                            continue;

                        foreach (var other in list)
                        {
                            if (other == obj)
                                continue;

                            NoDuplicateObjects.Add(other);
                        }
                    }

            results.AddRange(NoDuplicateObjects);
        }

        public void GetObjectsFromWorldPosition(Vector3 origin, List<CollisionObject> results)
        {
            results.Clear();

            Vector3Int cell = WorldToCell(origin);

            if (!cells.TryGetValue(cell, out var list)) return;

            results.AddRange(list);
        }

        public void GetObjectsFromCell(Vector3Int cell, List<CollisionObject> results)
        {
            results.Clear();

            if(!cells.TryGetValue(cell, out var list)) return;

            results.AddRange(list);
        }

        public void GetAllObjectsAlongRay(Vector3 origin, Vector3 direction, float distance, List<CollisionObject> results)
        {
            results.Clear();
            NoDuplicateObjects.Clear();

            Vector3 end = origin + direction * distance;

            Vector3Int current = WorldToCell(origin);
            Vector3Int target = WorldToCell(end);

            int stepX = direction.x >= 0 ? 1 : -1;
            int stepY = direction.y >= 0 ? 1 : -1;
            int stepZ = direction.z >= 0 ? 1 : -1;

            float nextBoundaryX = (current.x + (stepX > 0 ? 1 : 0)) * CELLSIZE;

            float nextBoundaryY = (current.y + (stepY > 0 ? 1 : 0)) * CELLSIZE;

            float nextBoundaryZ = (current.z + (stepZ > 0 ? 1 : 0)) * CELLSIZE;

            float tMaxX = direction.x != 0 ? (nextBoundaryX - origin.x) / direction.x : float.PositiveInfinity;

            float tMaxY = direction.y != 0 ? (nextBoundaryY - origin.y) / direction.y : float.PositiveInfinity;

            float tMaxZ = direction.z != 0 ? (nextBoundaryZ - origin.z) / direction.z : float.PositiveInfinity;

            float tDeltaX = direction.x != 0 ? CELLSIZE / Mathf.Abs(direction.x) : float.PositiveInfinity;

            float tDeltaY = direction.y != 0 ? CELLSIZE / Mathf.Abs(direction.y) : float.PositiveInfinity;

            float tDeltaZ = direction.z != 0 ? CELLSIZE / Mathf.Abs(direction.z) : float.PositiveInfinity;

            while(true)
            {
                if (cells.TryGetValue(current, out var list))
                {
                    foreach(var obj in list)
                    {
                        NoDuplicateObjects.Add(obj);
                    }
                }

                if (current == target) break;

                if (tMaxX < tMaxY)
                {
                    if (tMaxX < tMaxZ)
                    {
                        current.x += stepX;
                        tMaxX += tDeltaX;
                    }
                    else
                    {
                        current.z += stepZ;
                        tMaxZ += tDeltaZ;
                    }
                }
                else
                {
                    if (tMaxY < tMaxZ)
                    {
                        current.y += stepY;
                        tMaxY += tDeltaY;
                    }
                    else
                    {
                        current.z += stepZ;
                        tMaxZ += tDeltaZ;
                    }
                }
            }

            results.AddRange(NoDuplicateObjects);
        }

        private Vector3Int WorldToCell(Vector3 position)
        {
            return new Vector3Int(
                Mathf.FloorToInt(position.x / CELLSIZE),
                Mathf.FloorToInt(position.y / CELLSIZE),
                Mathf.FloorToInt(position.z / CELLSIZE));
        }

        private void RemoveObjFromOccupiedCells(CollisionObject obj)
        {
            foreach (var cell in obj.OccupiedCells)
            {
                if (cells.TryGetValue(cell, out var list))
                {
                    list.Remove(obj);

                    if (list.Count == 0)
                    {
                        cells.Remove(cell);
                    }
                }
            }

            obj.OccupiedCells.Clear();
        }

        private void SetOccupyingCells(CollisionObject obj, Vector3Int minCell, Vector3Int maxCell)
        {
            cellBuffer.Clear();

            for (int x = minCell.x; x <= maxCell.x; x++)
            {
                for (int y = minCell.y; y <= maxCell.y; y++)
                {
                    for (int z = minCell.z; z <= maxCell.z; z++)
                    {
                        Vector3Int cell = new(x, y, z);

                        if(!cells.TryGetValue(cell, out var list))
                        {
                            list = new List<CollisionObject>();
                            cells[cell] = list;
                        }

                        list.Add(obj);

                        cellBuffer.Add(cell);
                    }
                }
            }
            obj.OccupiedCells.AddRange(cellBuffer);
        }
    }
}