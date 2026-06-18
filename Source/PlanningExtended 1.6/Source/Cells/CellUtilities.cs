using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Cells;

public static class CellUtilities
{
    public static AreaDimensions DetermineAreaDimensions(IEnumerable<IntVec3> cells)
    {
        int minX = -1000;
        int minZ = -1000;
        int maxX = -1000;
        int maxZ = -1000;

        foreach (IntVec3 cell in cells)
        {
            if (minX == -1000 || cell.x < minX)
                minX = cell.x;
            if (cell.x > maxX)
                maxX = cell.x;

            if (minZ == -1000 || cell.z < minZ)
                minZ = cell.z;
            if (cell.z > maxZ)
                maxZ = cell.z;
        }

        return new AreaDimensions(minX, minZ, maxX, maxZ);
    }

    public static IEnumerable<IntVec3> GetCells(AreaDimensions areaDimensions)
    {
        for (int z = areaDimensions.MinZ; z <= areaDimensions.MaxZ; z++)
            for (int x = areaDimensions.MinX; x <= areaDimensions.MaxX; x++)
                yield return new IntVec3(x, 0, z);
    }

    public static HashSet<IntVec3> ClearCells(AreaDimensions areaDimensions, Map map)
    {
        HashSet<IntVec3> removedCells = [];

        foreach (var cell in GetCells(areaDimensions))
            if (map.designationManager.RemovePlanDesignationsAt(cell))
                removedCells.Add(cell);

        return removedCells;
    }

    public static List<IntVec3> GetCenterModeCells(IEnumerable<IntVec3> dragCells, IntVec3 cursor)
    {
        AreaDimensions areaDimensions = DetermineAreaDimensions(dragCells);

        if (!areaDimensions.IsValid)
            return [.. dragCells];

        // If cursor is at max, start is at min, otherwise start is at max
        int startX = (cursor.x == areaDimensions.MaxX) ? areaDimensions.MinX : areaDimensions.MaxX;
        int startZ = (cursor.z == areaDimensions.MaxZ) ? areaDimensions.MinZ : areaDimensions.MaxZ;

        int halfWidth = areaDimensions.MaxX - areaDimensions.MinX;
        int halfHeight = areaDimensions.MaxZ - areaDimensions.MinZ;

        int newMinX = startX - halfWidth;
        int newMaxX = startX + halfWidth;
        int newMinZ = startZ - halfHeight;
        int newMaxZ = startZ + halfHeight;

        List<IntVec3> result = [];
        
        for (int z = newMinZ; z <= newMaxZ; z++)
            for (int x = newMinX; x <= newMaxX; x++)
                result.Add(new IntVec3(x, 0, z));

        return result;
    }
}
