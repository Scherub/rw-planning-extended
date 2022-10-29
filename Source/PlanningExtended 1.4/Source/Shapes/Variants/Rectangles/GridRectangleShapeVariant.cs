using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    public class GridRectangleShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.GridRectangle;

        public override ShapeOptions SecondShapeOption => ShapeOptions.NumberOfSegments;

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
