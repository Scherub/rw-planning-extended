using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended
{
    internal static class AreaDimensionsExtensions
    {
        public static Direction GetDirection(this AreaDimensions areaDimensions, IntVec3 position)
        {
            if (position.x >= areaDimensions.RealCenterX && position.z >= areaDimensions.RealCenterZ)
                return Direction.NorthEast;
            else if (position.x >= areaDimensions.RealCenterX && position.z <= areaDimensions.RealCenterZ)
                return Direction.SouthEast;
            if (position.x <= areaDimensions.RealCenterX && position.z >= areaDimensions.RealCenterZ)
                return Direction.NorthWest;
            else if (position.x <= areaDimensions.RealCenterX && position.z <= areaDimensions.RealCenterZ)
                return Direction.SouthWest;

            return Direction.None;
        }

        public static IntVec3 GetPosition(this AreaDimensions areaDimensions, Direction direction)
        {
            return direction switch
            {
                Direction.North => new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MaxZ),
                Direction.NorthEast => new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MaxZ),
                Direction.East => new IntVec3(areaDimensions.MaxX, 0, areaDimensions.CenterZ),
                Direction.SouthEast => new IntVec3(areaDimensions.MaxX, 0, areaDimensions.MinZ),
                Direction.South => new IntVec3(areaDimensions.CenterX, 0, areaDimensions.MinZ),
                Direction.SouthWest => new IntVec3(areaDimensions.MinX, 0, areaDimensions.MinZ),
                Direction.West => new IntVec3(areaDimensions.MinX, 0, areaDimensions.CenterZ),
                Direction.NorthWest => new IntVec3(areaDimensions.MinX, 0, areaDimensions.MaxZ),
                _ => areaDimensions.Center
            };
        }

        public static IntVec3 GetStartPosition(this AreaDimensions areaDimensions, IntVec3 endPosition)
        {
            Direction endPointDirection = areaDimensions.GetDirection(endPosition);
            Direction startPointDirection = endPointDirection.GetOpposite();

            return areaDimensions.GetPosition(startPointDirection);

        }
    }
}
