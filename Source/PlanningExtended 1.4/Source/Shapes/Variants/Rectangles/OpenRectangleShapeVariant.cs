using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    public class OpenRectangleShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.OpenRectangle;

        public override ShapeOptions SecondShapeOption => ShapeOptions.Thickness;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            if (cell.x == areaDimensions.MinX || cell.x == areaDimensions.MaxX)
                return true;

            if (cell.z == areaDimensions.MinZ || cell.z == areaDimensions.MaxZ)
                return true;

            return false;
        }
    }
}
