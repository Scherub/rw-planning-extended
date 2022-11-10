using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Rectangles
{
    internal class FilledRectangleShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.FilledRectangle;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
