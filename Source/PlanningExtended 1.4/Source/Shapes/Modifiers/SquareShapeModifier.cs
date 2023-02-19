using System;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class SquareShapeModifier : BaseShapeModifier
    {
        public override AreaDimensions Update(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            int minX = areaDimensions.MinX;
            int minZ = areaDimensions.MinZ;
            int maxX = areaDimensions.MaxX;
            int maxZ = areaDimensions.MaxZ;

            int length = Math.Min(areaDimensions.Width, areaDimensions.Height);

            Direction targetDirection = areaDimensions.GetDirection(mousePosition);

            if (targetDirection is Direction.NorthEast)
            {
                maxX = areaDimensions.MinX + length;
                maxZ = areaDimensions.MinZ + length;
            }
            else if (targetDirection is Direction.SouthEast)
            {
                maxX = areaDimensions.MinX + length;
                minZ = areaDimensions.MaxZ - length;
            }
            else if (targetDirection is Direction.SouthWest)
            {
                minX = areaDimensions.MaxX - length;
                minZ = areaDimensions.MaxZ - length;
            }
            else if (targetDirection is Direction.NorthWest)
            {
                minX = areaDimensions.MaxX - length;
                maxZ = areaDimensions.MinZ + length;
            }

            return new(minX, minZ, maxX, maxZ);
        }
    }
}
