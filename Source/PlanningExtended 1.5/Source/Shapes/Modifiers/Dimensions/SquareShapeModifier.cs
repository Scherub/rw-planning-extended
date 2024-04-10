using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Modifiers.Dimensions
{
    internal class SquareShapeModifier : BasePolygonShapeModifier
    {
        protected override IntVec3 DetermineNewSize(AreaDimensions areaDimensions, Direction rotation)
        {
            return areaDimensions.Width < areaDimensions.Height ? new(areaDimensions.Width - 1, 0, areaDimensions.Width - 1) : new(areaDimensions.Height - 1, 0, areaDimensions.Height - 1);
        }
    }
}
