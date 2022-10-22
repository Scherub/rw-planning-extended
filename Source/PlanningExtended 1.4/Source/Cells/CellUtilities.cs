using System.Collections.Generic;
using Verse;

namespace PlanningExtended.Cells
{
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

        public static void ClearCells(AreaDimensions areaDimensions, Map map)
        {
            foreach (var cell in GetCells(areaDimensions))
                map.designationManager.RemovePlanDesignationsAt(cell);
        }
    }
}
