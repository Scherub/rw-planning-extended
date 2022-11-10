using PlanningExtended.Cells;
using Verse;

namespace PlanningExtended.Shapes.Variants.Lines
{
    public class SimpleLineShapeVariant : BaseShapeVariant
    {
        public override ShapeVariant ShapeVariant => ShapeVariant.SimpleLine;

        public override ShapeOptions SecondShapeOption => ShapeOptions.Thickness;

        public override bool IsCellValid(IntVec3 cell, AreaDimensions areaDimensions)
        {
            return true;
        }
    }
}
