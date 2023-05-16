using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions
{
    internal class LineSimpleShapeModifier : BaseShapeDimensionsModifier
    {
        public override AreaDimensions Update(BaseShape shape, AreaDimensions areaDimensions, IntVec3 mousePosition, Direction rotation)
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
