using System;
using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal abstract class BasePolygonShapeModifier : BaseShapeDimensionsModifier
    {
        public override AreaDimensions Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation)
        {
            int minX = areaDimensions.MinX;
            int minZ = areaDimensions.MinZ;
            int maxX = areaDimensions.MaxX;
            int maxZ = areaDimensions.MaxZ;

            IntVec3 newSize = DetermineNewSize(areaDimensions, rotation);

            Direction targetDirection = areaDimensions.GetDirection(mousePosition);

            return targetDirection switch
            {
                Direction.NorthEast => new AreaDimensions(minX, minZ, minX + newSize.x, minZ + newSize.z),
                Direction.SouthEast => new AreaDimensions(minX, maxZ - newSize.z, minX + newSize.x, maxZ),
                Direction.SouthWest => new AreaDimensions(maxX - newSize.x, maxZ - newSize.z, maxX, maxZ),
                Direction.NorthWest => new AreaDimensions(maxX - newSize.x, minZ, maxX, minZ + newSize.z),
                _ => throw new NotImplementedException(),
            };
        }

        protected abstract IntVec3 DetermineNewSize(AreaDimensions areaDimensions, Direction rotation);
    }
}
