using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers
{
    internal class LineShapeModifier : BaseShapeModifier
    {
        public override AreaDimensions Update(AreaDimensions areaDimensions, IntVec3 mousePosition)
        {
            IntVec3 endPosition = new(mousePosition.x, 0, mousePosition.z);
            IntVec3 startPosition = areaDimensions.GetStartPosition(endPosition);

            if (areaDimensions.Width > areaDimensions.Height)
                return new AreaDimensions(areaDimensions.MinX, startPosition.z, areaDimensions.MaxX, startPosition.z);
            else if (areaDimensions.Width < areaDimensions.Height)
                return new AreaDimensions(startPosition.x, areaDimensions.MinZ, startPosition.x, areaDimensions.MaxZ);

            return areaDimensions;
        }
    }
}
